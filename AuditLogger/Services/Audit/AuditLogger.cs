using System;
using System.Collections.Generic;
using System.Linq;

namespace TwoWayRelation.Services.Audit
{
    public class AuditLogger
    {
        private readonly List<string> changes = new List<string>();
        private void AddChange(int indent, string change)
        {
            changes.Add($"{string.Join("", Enumerable.Range(0, indent).Select(_ => ' '))}{change}");
        }
        public void Changed(string property, object from, object to, int indent)
        {
            AddChange(indent, $"{property}: {from} -> {to}");
        }

        public void Changed(string property, string change, int indent)
        {
            AddChange(indent, $"{property}: {change}");
        }

        public string GetChanges()
        {
            return string.Join(Environment.NewLine, changes);
        }
    }
}
