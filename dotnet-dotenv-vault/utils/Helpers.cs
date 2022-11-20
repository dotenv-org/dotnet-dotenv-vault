﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dotnet_dotenv_vault
{
    internal static class Helpers
    {
        private static ReadOnlySpan<KeyValuePair<string, string>> ReadAndParse(string envFilePath,
            bool ignoreExceptions, Encoding encoding, bool trimValues)
        {
            var rawEnvRows = Reader.Read(envFilePath, ignoreExceptions, encoding);

            if (rawEnvRows == ReadOnlySpan<string>.Empty)
            {
                return ReadOnlySpan<KeyValuePair<string, string>>.Empty;
            }

            return Parser.Parse(rawEnvRows, trimValues);
        }

        internal static IDictionary<string, string> ReadAndReturn(LoadOptions options)
        {
            var response = new Dictionary<string, string>();
            var envFilePaths = options.ProbeForEnv
                ? new[] { GetProbedEnvPath(options.ProbeLevelsToSearch, options.IgnoreExceptions) }
                : options.EnvFilePaths;

            foreach (var envFilePath in envFilePaths)
            {
                var envRows = ReadAndParse(envFilePath, options.IgnoreExceptions, options.Encoding,
                    options.TrimValues);

                foreach (var envRow in envRows)
                {
                    if (response.ContainsKey(envRow.Key))
                    {
                        response[envRow.Key] = envRow.Value;
                    }
                    else
                    {
                        response.Add(envRow.Key, envRow.Value);
                    }
                }
            }

            return response;
        }

        internal static void ReadAndWrite(LoadOptions options)
        {
            var envVars = ReadAndReturn(options);

            foreach (var envVar in envVars)
            {
                if (options.OverwriteExistingVars)
                {
                    Environment.SetEnvironmentVariable(envVar.Key, envVar.Value);
                }
                else if (!EnvReader.HasValue(envVar.Key))
                {
                    Environment.SetEnvironmentVariable(envVar.Key, envVar.Value);
                }
            }
        }

        private static string GetProbedEnvPath(int levelsToSearch, bool ignoreExceptions)
        {
            var currentDirectory = new DirectoryInfo(AppContext.BaseDirectory);
            var count = levelsToSearch;
            var foundEnvPath = SearchPaths();

            if (string.IsNullOrEmpty(foundEnvPath) && !ignoreExceptions)
            {
                //throw new FileNotFoundException(
                //    $"Failed to find a file matching the '{DotEnvOptions.DefaultEnvFileName}' search pattern." +
                //    $"{Environment.NewLine}Current Directory: {currentDirectory}" +
                //    $"{Environment.NewLine}Levels Searched: {levelsToSearch}");
            }

            return foundEnvPath;


            string SearchPaths()
            {
                for (;
                    currentDirectory != null && count > 0;
                    count--, currentDirectory = currentDirectory.Parent)
                {
                    //foreach (var file in currentDirectory.GetFiles(DotEnvOptions.DefaultEnvFileName,
                    //    SearchOption.TopDirectoryOnly))
                    //{
                    //    return file.FullName;
                    //}
                }

                return null;
            }
        }
    }
}