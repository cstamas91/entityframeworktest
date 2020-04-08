using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;
using System.Collections.Generic;
using TwoWayRelation.Data.Models;

namespace AuditLogger.Abstraction
{
    public interface IAuditableVisitor<TRoot>
        where TRoot : IAuditableEntityRoot<TRoot>
    {
        void Audit(TRoot auditableRoot, DbContext dbContext);
        void LogChange<TAttrib>(string attribute, TAttrib from, TAttrib to);
        void LogChange<TAttrib>(string attribute, TAttrib from);
        IEnumerable<AuditLogRecord> GetLogRecords();
    }

    public interface IAuditableEntityRoot<TRoot>
        where TRoot : IAuditableEntityRoot<TRoot>
    {
        void Visit(IAuditableVisitor<TRoot> logger, DbContext dbContext);
    }

    public interface IAuditableEntityNode<TRoot> 
        where TRoot : IAuditableEntityRoot<TRoot>
    {
        void Visit(IAuditableVisitor<TRoot> logger, DbContext dbContext, string navigation);
    }


    public static class AuditHelpers
    {
        public static void LogAttributeIfChanged<TRoot, TAttrib>(
            IAuditableVisitor<TRoot> visitor, 
            EntityEntry entityEntry, 
            string attribute, 
            string propertyName,
            TAttrib value)
            where TRoot : IAuditableEntityRoot<TRoot>
        {
            var original = (TAttrib)entityEntry.OriginalValues[propertyName];
            if (!original.Equals(value))
            {
                visitor.LogChange(attribute, original, value);
            }
        }
    }
}
