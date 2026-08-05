using AutoMapper;
using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryTeamDTOs;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.SurgeryService.SurgeryTeamService
{
    public class SurgeryTeamService : ISurgeryTeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SurgeryTeamService> _logger;

        public SurgeryTeamService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<SurgeryTeamService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<SurgeryTeamResponseDto>>> GetAllSurgeryTeamsAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.SurgeryTeams.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<SurgeryTeamResponseDto>>(items);

                var pagedResult = new PagedResultDto<SurgeryTeamResponseDto>
                {
                    Items = mappedItems,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return new ApiResponseDto<PagedResultDto<SurgeryTeamResponseDto>>
                {
                    Message = "Surgery teams retrieved successfully.",
                    Data = pagedResult
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all surgery teams.");
                throw;
            }
        }

        public async Task<ApiResponseDto<SurgeryTeamResponseDto>> GetSurgeryTeamByIdAsync(int id)
        {
            try
            {
                var teamMember = await _unitOfWork.SurgeryTeams.GetByIdAsync(id);

                if (teamMember == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Surgery Team Member {TeamId}.", id);
                    throw new KeyNotFoundException("The surgery team member does not exist.");
                }

                return new ApiResponseDto<SurgeryTeamResponseDto>
                {
                    Message = "Surgery team member retrieved successfully.",
                    Data = _mapper.Map<SurgeryTeamResponseDto>(teamMember)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Surgery Team Member {TeamId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<SurgeryTeamResponseDto>>> GetTeamBySurgeryIdAsync(int surgeryId)
        {
            try
            {
                var team = await _unitOfWork.SurgeryTeams.GetTeamBySurgeryIdAsync(surgeryId);

                return new ApiResponseDto<IEnumerable<SurgeryTeamResponseDto>>
                {
                    Message = "Surgery team retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<SurgeryTeamResponseDto>>(team)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving team for Surgery {SurgeryId}.", surgeryId);
                throw;
            }
        }

        public async Task<ApiResponseDto<SurgeryTeamResponseDto>> CreateSurgeryTeamAsync(CreateSurgeryTeamDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var surgery = await _unitOfWork.SurgeryRecords.GetByIdAsync(dto.SurgeryId);
                if (surgery == null)
                    throw new KeyNotFoundException("العملية غير موجودة.");

                var staff = await _unitOfWork.Staff.GetByIdAsync(dto.StaffId);
                if (staff == null)
                    throw new KeyNotFoundException("الموظف غير موجود.");

                var teamMember = _mapper.Map<SurgeryTeam>(dto);
                await _unitOfWork.SurgeryTeams.AddAsync(teamMember);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully added Staff {StaffId} to Surgery {SurgeryId}.", dto.StaffId, dto.SurgeryId);
                await transaction.CommitAsync();

                return new ApiResponseDto<SurgeryTeamResponseDto>
                {
                    Message = "Team member added to surgery successfully.",
                    Data = _mapper.Map<SurgeryTeamResponseDto>(teamMember)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while adding a team member to surgery.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateSurgeryTeamAsync(UpdateSurgeryTeamDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var teamMember = await _unitOfWork.SurgeryTeams.GetByIdAsync(dto.Id);
                if (teamMember == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Surgery Team Member {TeamId}.", dto.Id);
                    throw new KeyNotFoundException("The surgery team member does not exist.");
                }

                var surgery = await _unitOfWork.SurgeryRecords.GetByIdAsync(dto.SurgeryId);
                if (surgery == null) throw new KeyNotFoundException("العملية غير موجودة.");

                var staff = await _unitOfWork.Staff.GetByIdAsync(dto.StaffId);
                if (staff == null) throw new KeyNotFoundException("الموظف غير موجود.");

                _mapper.Map(dto, teamMember);

                _unitOfWork.SurgeryTeams.Update(teamMember);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Surgery Team Member {TeamId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Surgery team member updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Surgery Team Member {TeamId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteSurgeryTeamAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var teamMember = await _unitOfWork.SurgeryTeams.GetByIdAsync(id);

                if (teamMember == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Surgery Team Member {TeamId}.", id);
                    throw new KeyNotFoundException("The surgery team member does not exist.");
                }

                teamMember.IsDeleted = true;

                _unitOfWork.SurgeryTeams.Update(teamMember);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully soft-deleted Surgery Team Member {TeamId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Surgery team member deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Surgery Team Member {TeamId}.", id);
                throw;
            }
        }
    }
}