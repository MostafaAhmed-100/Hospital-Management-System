using AutoMapper;
using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.DTOs.InvoiceDTOs;
using HospitalManagementSystem.DTOs.Shared;
using HospitalManagementSystem.Repository.UnitofWork;

namespace HospitalManagementSystem.Service.InvoiceService
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceService> _logger;

        public InvoiceService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<InvoiceService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponseDto<PagedResultDto<InvoiceResponseDto>>> GetAllInvoicesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var (items, totalCount) = await _unitOfWork.Invoices.GetAllPagedAsync(pageNumber, pageSize);
                var mappedItems = _mapper.Map<IEnumerable<InvoiceResponseDto>>(items);

                return new ApiResponseDto<PagedResultDto<InvoiceResponseDto>>
                {
                    Message = "Invoices retrieved successfully.",
                    Data = new PagedResultDto<InvoiceResponseDto>
                    {
                        Items = mappedItems,
                        TotalCount = totalCount,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all invoices.");
                throw;
            }
        }

        public async Task<ApiResponseDto<InvoiceResponseDto>> GetInvoiceByIdAsync(int id)
        {
            try
            {
                var invoice = await _unitOfWork.Invoices.GetByIdAsync(id);

                if (invoice == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Invoice {InvoiceId}.", id);
                    throw new KeyNotFoundException("The invoice does not exist.");
                }

                return new ApiResponseDto<InvoiceResponseDto>
                {
                    Message = "Invoice retrieved successfully.",
                    Data = _mapper.Map<InvoiceResponseDto>(invoice)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving Invoice {InvoiceId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<InvoiceWithPaymentsResponseDto>> GetInvoiceWithPaymentsAsync(int id)
        {
            try
            {
                var invoice = await _unitOfWork.Invoices.GetInvoiceWithPaymentsAsync(id);

                if (invoice == null)
                {
                    _logger.LogWarning("Attempted to retrieve non-existent Invoice {InvoiceId} with payments.", id);
                    throw new KeyNotFoundException("The invoice does not exist.");
                }

                return new ApiResponseDto<InvoiceWithPaymentsResponseDto>
                {
                    Message = "Invoice with payments retrieved successfully.",
                    Data = _mapper.Map<InvoiceWithPaymentsResponseDto>(invoice)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving payments for Invoice {InvoiceId}.", id);
                throw;
            }
        }

        public async Task<ApiResponseDto<IEnumerable<InvoiceResponseDto>>> GetUnpaidInvoicesByPatientAsync(int patientId)
        {
            try
            {
                var invoices = await _unitOfWork.Invoices.GetUnpaidInvoicesByPatientAsync(patientId);

                return new ApiResponseDto<IEnumerable<InvoiceResponseDto>>
                {
                    Message = "Unpaid invoices retrieved successfully.",
                    Data = _mapper.Map<IEnumerable<InvoiceResponseDto>>(invoices)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving unpaid invoices for Patient {PatientId}.", patientId);
                throw;
            }
        }

        public async Task<ApiResponseDto<InvoiceResponseDto>> CreateInvoiceAsync(CreateInvoiceDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var patient = await _unitOfWork.Patients.GetByIdAsync(dto.PatientId);
                if (patient == null) throw new KeyNotFoundException("The specified patient does not exist.");

                var appointment = await _unitOfWork.Appointments.GetByIdAsync(dto.AppointmentId);
                if (appointment == null) throw new KeyNotFoundException("The specified appointment does not exist.");

                var invoice = _mapper.Map<Invoice>(dto);
                invoice.Status = InvoiceStatus.Unpaid;

                await _unitOfWork.Invoices.AddAsync(invoice);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully created a new Invoice {InvoiceId}.", invoice.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<InvoiceResponseDto>
                {
                    Message = "Invoice created successfully.",
                    Data = _mapper.Map<InvoiceResponseDto>(invoice)
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while creating a new invoice.");
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> UpdateInvoiceAsync(UpdateInvoiceDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var invoice = await _unitOfWork.Invoices.GetByIdAsync(dto.Id);
                if (invoice == null)
                {
                    _logger.LogWarning("Attempted to update non-existent Invoice {InvoiceId}.", dto.Id);
                    throw new KeyNotFoundException("The invoice does not exist.");
                }

                _mapper.Map(dto, invoice);

                _unitOfWork.Invoices.Update(invoice);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully updated Invoice {InvoiceId}.", dto.Id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Invoice updated successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while updating Invoice {InvoiceId}.", dto.Id);
                throw;
            }
        }

        public async Task<ApiResponseDto<string>> DeleteInvoiceAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var invoice = await _unitOfWork.Invoices.GetByIdAsync(id);

                if (invoice == null)
                {
                    _logger.LogWarning("Attempted to delete non-existent Invoice {InvoiceId}.", id);
                    throw new KeyNotFoundException("The invoice does not exist.");
                }

                _unitOfWork.Invoices.Delete(invoice);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted Invoice {InvoiceId}.", id);
                await transaction.CommitAsync();

                return new ApiResponseDto<string>
                {
                    Message = "Invoice deleted successfully.",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error occurred while deleting Invoice {InvoiceId}.", id);
                throw;
            }
        }
    }
}