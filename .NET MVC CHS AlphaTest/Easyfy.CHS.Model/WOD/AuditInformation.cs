using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easyfy.CHS.Model.Wod
{
    [Serializable]
    public class AuditInformation
    {
        private DateTime _modifiedOn;
        private DateTime _createdOn;
        private DateTime _administeredOn;

        public AuditInformation()
        {
            ModifiedOn = DateTime.Now;
            CreatedOn = DateTime.Now;
        }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get { return _modifiedOn.ToLocalTime(); } set { _modifiedOn = value.ToUniversalTime(); } }
        public DateTime CreatedOn { get { return _createdOn.ToLocalTime(); } set { _createdOn = value.ToUniversalTime(); } }
        public string AdministeredBy { get; set; }
        public DateTime AdministeredOn { get { return _administeredOn.ToLocalTime(); } set { _administeredOn = value.ToUniversalTime(); } }
    }
}
