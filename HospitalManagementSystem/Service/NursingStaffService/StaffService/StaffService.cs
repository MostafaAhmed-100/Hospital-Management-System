using AutoMapper;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.NursingStaffService.StaffService
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<StaffService> _logger;

        public StaffService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<StaffService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<StaffResponseDto>>> GetAllStaffAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Staff.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<StaffResponseDto>>(items);

                var pagedResult = new PagedResultDto<StaffResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<StaffResponseDto>>
                {
                    Message = "Staff retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all staff.");
                throw;
            }
        }

        public async Task<ApiResponseDto<StaffResponseDto>> GetStaffByIdAsync(int id)
        {
            try
            {
                var staffMember = await _unitOfWork.Staff.GetByIdAsync(id);

                if (staffMember == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Staff {StaffId}.", id);
                    throw new KeyNotFoundException("The staff member does not exist.");
                }

                return new ApiResponseDto<StaffResponseDto>
                {
                    Message = "Staff retrieved successfully.",
                    Data = _mapper.Map<StaffResponseDto>(staffMember)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Staff {StaffId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<StaffResponseDto>>> GetStaffByClinicIdAsync(int clinicId)
        {
            try
            {
                var staffList = await _unitOfWork.Staff.GetStaffByClinicIdAsync(clinicId);

                return new ApiResponseDto<IEnumerable<StaffResponseDto>>
                {
                    Message = "Staff retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<StaffResponseDto>>(staffList)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving staff for Clinic {ClinicId}.", clinicId);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<StaffResponseDto>>> GetStaffByRoleAsync(StaffRole role)
        {
            try
            {
                var staffList = await _unitOfWork.Staff.GetStaffByRoleAsync(role);

                return new ApiResponseDto<IEnumerable<StaffResponseDto>>
                {
                    Message = "Staff retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<StaffResponseDto>>(staffList)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving staff with Role {Role}.", role.ToString());
                throw;
            }
        }

        public async Task<ApiResponseDto<StaffResponseDto>> CreateStaffAsync(CreateStaffDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var clinic = await _unitOfWork.Clinics.GetByIdAsync(dto.ClinicId);
                if (clinic == null)
                    throw new KeyNotFoundException("العيادة غير موجودة.");

                var staff = _mapper.Map<Staff>(dto);
                await _unitOfWork.Staff.AddAsync(staff);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Staff Member {StaffId}.", staff.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<StaffResponseDto>
                {
                    Message = "Staff created successfully.",
                    Data = _mapper.Map<StaffResponseDto>(staff)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new staff member.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateStaffAsync(UpdateStaffDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var staff = await _unitOfWork.Staff.GetByIdAsync(dto.Id);
                if (staff == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Staff {StaffId}.", dto.Id);
                    throw new KeyNotFoundException("The staff member does not exist.");
                }

                var clinic = await _unitOfWork.Clinics.GetByIdAsync(dto.ClinicId);
                if (clinic == null)
                    throw new KeyNotFoundException("العيادة غير موجودة.");

                _mapper.Map(dto, staff);

                _unitOfWork.Staff.Update(staff);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Staff {StaffId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Staff updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Staff {StaffId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteStaffAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var staff = await _unitOfWork.Staff.GetByIdAsync(id);

                if (staff == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Staff {StaffId}.", id);
                    throw new KeyNotFoundException("The staff member does not exist.");
                }

                staff.IsDeleted = true;

                _unitOfWork.Staff.Update(staff);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Staff {StaffId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Staff deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Staff {StaffId}.", id);
                throw;
            }
        }
    }
}