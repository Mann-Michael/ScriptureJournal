using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ScripJournal.Models
{
    public class ScripJournalContext : DbContext
    {
        public ScripJournalContext (DbContextOptions<ScripJournalContext> options)
            : base(options)
        {
        }

        public DbSet<ScripJournal.Models.Scripture> Scripture { get; set; }
    }
}
