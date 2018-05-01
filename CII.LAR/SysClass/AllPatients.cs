using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CII.LAR.SysClass
{
    [Serializable]
    public class AllPatients
    {
        public static AllPatients allPatients;

        private List<Patient> patients;
        public List<Patient> Patients
        {
            get
            {
                return patients;
            }
            private set
            {
                patients = value;
            }
        }

        public AllPatients()
        {
            patients = new List<Patient>();
        }

        public static AllPatients GetAllPatients()
        {
            if (allPatients == null)
            {
                allPatients = new AllPatients();
            }
            return allPatients;
        }

        public void Add(Patient patient)
        {
            patients.Add(patient);
        }

        public int Count
        {
            get
            {
                return patients.Count;
            }
        }
    }
}
