using System;
using System.Collections.Generic;
using System.Text;

namespace dotenv_vault.net
{
    class LoadOptions
    {
        public bool OverwriteExistingVars { get; }
        public bool TrimValues { get; }
        public Encoding Encoding { get; }
        public IEnumerable<string> EnvFilePaths { get; }
        public bool IgnoreExceptions { get; }
        public bool ProbeForEnv { get; }
        public int ProbeLevelsToSearch { get; }

        public static readonly string DefaultEnvFileName = ".env";

        public LoadOptions()
        {
            // Defaults
            OverwriteExistingVars = true;
            TrimValues = false;
            Encoding = Encoding.ASCII;
            EnvFilePaths = null;
            IgnoreExceptions = true;
            ProbeForEnv = false;
            ProbeLevelsToSearch = 4;
        }
    }
}
