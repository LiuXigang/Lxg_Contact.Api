﻿using System;
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

        public async Task<bool> AddRequestAsync(ContactApplyRequest request, CancellationToken cancellationToken)
        {
            //声明是否重复的filterdefinition 和 updatedefinition
            var filter = Builders<ContactApplyRequest>.Filter
                .Where(n => n.UserId == request.UserId && n.ApplierId == request.ApplierId);
            var count = await _context.ContactApplyRequests.CountDocumentsAsync(filter);
            if (count > 0)
            {
                var update = Builders<ContactApplyRequest>.Update.Set(r => r.ApplyTime, DateTime.Now);
                var result = await _context.ContactApplyRequests.UpdateOneAsync(filter, update);
                return result.MatchedCount == result.ModifiedCount && result.ModifiedCount == 1;
            }
            await _context.ContactApplyRequests.InsertOneAsync(request, null, cancellationToken);
            return true;
        }

        public async Task<bool> ApprovalAsync(int userId, int applierId, CancellationToken cancellationToken)
        {
            var filter = Builders<ContactApplyRequest>.Filter
                .Where(r => r.UserId == userId && r.ApplierId == applierId);
            var update = Builders<ContactApplyRequest>.Update
                .Set(r => r.ApplyTime, DateTime.Now)
                .Set(r => r.HandleTime, DateTime.Now)
                .Set(r => r.Approvaled, 1);
            var result = await _context.ContactApplyRequests.UpdateOneAsync(filter, update);
            return result.MatchedCount == result.ModifiedCount && result.ModifiedCount == 1;
        }

        public async Task<List<ContactApplyRequest>> GetRequestListAsync(int userId, CancellationToken cancellationToken)
        {
            var data = await _context.ContactApplyRequests.FindAsync(r => r.UserId == userId);
            return data.ToList(cancellationToken);
        }
    }
}
