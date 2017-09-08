using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candidate.Data
{
    public class CandidateRepository
    {
        private string _connectionString;
        public CandidateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddCandidate(Applicant applicant)
        {
            applicant.Status = Status.Pending;
            using(var context = new CandidatesDataContext(_connectionString))
            {
                context.Applicants.InsertOnSubmit(applicant);
                context.SubmitChanges();
            }
        }
        public IEnumerable<Applicant> GetPending()
        {
            using (var context = new CandidatesDataContext(_connectionString))
            {
                return context.Applicants.Where(a => a.Status == Status.Pending).ToList();
            }
        }
        public IEnumerable<Applicant> GetConfirmed()
        {
            using (var context = new CandidatesDataContext(_connectionString))
            {
                return context.Applicants.Where(a => a.Status == Status.Confirmed).ToList();
            }
        }
        public IEnumerable<Applicant> GetRejected()
        {
            using (var context = new CandidatesDataContext(_connectionString))
            {
                return context.Applicants.Where(a => a.Status == Status.Rejected).ToList();
            }
        }
        public void Update(Status s, int id)
        {
            using (var context = new CandidatesDataContext(_connectionString))
            {
                context.ExecuteCommand("UPDATE Candidates SET Status = {0} WHERE Id = {1}", s, id);
            }
        }
    }
}
