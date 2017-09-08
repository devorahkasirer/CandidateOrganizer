using Candidate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Candidate.Web.Models
{
    public class CandidatesViewModel
    {
        public IEnumerable<Applicant> Candidates { get; set; }
        public Status Decision { get; set; }
    }
}