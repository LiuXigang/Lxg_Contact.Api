using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contact.API.Data;
using Contact.API.Models;
using MongoDB.Driver;

namespace Contact.API.Repository
{
    public class MongoContactApplyRequestRepository : IContactApplyRequestRepository
    {
        private ContactContext _context;
        public MongoContactApplyRequestRepository(ContactContext context) => _context = context;

        public Task<bool> AddRequestAsync(ContactApplyRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApprovalAsync(int userId, int applierId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContactApplyRequest>> GetRequestListAsync(int userId, CancellationToken cancellationToken)
        {
            var data = await _context.ContactApplyRequests.FindAsync(r => r.UserId == userId);
            return data.ToList(cancellationToken);
        }
    }
}
