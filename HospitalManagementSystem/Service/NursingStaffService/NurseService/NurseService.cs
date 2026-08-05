using AutoMapper;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Nursing_Staff;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseDTOs;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.NursingStaffService.NurseService
{
    public class NurseService : INurseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<NurseService> _logger;

        public NurseService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<NurseService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<NurseResponseDto>>> GetAllNursesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Nurses.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<NurseResponseDto>>(items);

                var pagedResult = new PagedResultDto<NurseResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<NurseResponseDto>>
                {
                    Message = "Nurses retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all nurses.");
                throw;
            }
        }

        public async Task<ApiResponseDto<NurseResponseDto>> GetNurseByIdAsync(int id)
        {
            try
            {
                var nurse = await _unitOfWork.Nurses.GetByIdAsync(id);

                if (nurse == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Nurse {NurseId}.", id);
                    throw new KeyNotFoundException("The nurse does not exist.");
                }

                return new ApiResponseDto<NurseResponseDto>
                {
                    Message = "Nurse retrieved successfully.",
                    Data = _mapper.Map<NurseResponseDto>(nurse)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Nurse {NurseId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<NurseResponseDto>>> GetNursesByShiftAsync(ShiftType shift)
        {
            try
            {
                var nurses = await _unitOfWork.Nurses.GetNursesByShiftAsync(shift);

                return new ApiResponseDto<IEnumerable<NurseResponseDto>>
                {
                    Message = "Nurses retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<NurseResponseDto>>(nurses)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving nurses for Shift {Shift}.", shift.ToString());
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<NurseResponseDto>>> GetNursesByWardAsync(string wardSpecialization)
        {
            try
            {
                var nurses = await _unitOfWork.Nurses.GetNursesByWardAsync(wardSpecialization);

                return new ApiResponseDto<IEnumerable<NurseResponseDto>>
                {
                    Message = "Nurses retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<NurseResponseDto>>(nurses)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving nurses for Ward {Ward}.", wardSpecialization);
                throw;
            }
        }

        public async Task<ApiResponseDto<NurseResponseDto>> CreateNurseAsync(CreateNurseDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var staff = await _unitOfWork.Staff.GetByIdAsync(dto.StaffId);
                if (staff == null)
                    throw new KeyNotFoundException("الموظف غير موجود.");

                if (staff.Role != StaffRole.Nurse)
                    throw new InvalidOperationException("الموظف المحدد ليس مسجلاً كممرض في النظام.");

                var nurse = _mapper.Map<Nurse>(dto);
                await _unitOfWork.Nurses.AddAsync(nurse);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Nurse profile {NurseId} for Staff {StaffId}.", nurse.Id, dto.StaffId);
                await transaction.CommitAsync();

                return new ApiResponseDto<NurseResponseDto>
                {
                    Message = "Nurse created successfully.",
                    Data = _mapper.Map<NurseResponseDto>(nurse)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new nurse.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateNurseAsync(UpdateNurseDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var nurse = await _unitOfWork.Nurses.GetByIdAsync(dto.Id);
                if (nurse == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Nurse {NurseId}.", dto.Id);
                    throw new KeyNotFoundException("The nurse does not exist.");
                }

                var staff = await _unitOfWork.Staff.GetByIdAsync(dto.StaffId);
                if (staff == null)
                    throw new KeyNotFoundException("الموظف غير موجود.");

                _mapper.Map(dto, nurse);

                _unitOfWork.Nurses.Update(nurse);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Nurse {NurseId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Nurse updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Nurse {NurseId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteNurseAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var nurse = await _unitOfWork.Nurses.GetByIdAsync(id);

                if (nurse == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Nurse {NurseId}.", id);
                    throw new KeyNotFoundException("The nurse does not exist.");
                }

                nurse.IsDeleted = true;

                _unitOfWork.Nurses.Update(nurse);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Nurse {NurseId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Nurse deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Nurse {NurseId}.", id);
                throw;
            }
        }
    }
}