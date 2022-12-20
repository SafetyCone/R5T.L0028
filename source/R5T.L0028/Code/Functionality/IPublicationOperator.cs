using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0132;


namespace R5T.L0028
{
    [FunctionalityMarker]
    public partial interface IPublicationOperator : IFunctionalityMarker
    {
        /// <summary>
        /// Publishes an executable (not a library) to a named, timestamed, and current directory.
        /// </summary>
        /// <returns>
        /// The current output binaries  directory path.
        /// </returns>
        public async Task<string> Publish(
            string projectFilePath, 
            string binariesOutputDirectoryPath,
            ILogger logger)
        {
            logger.LogInformation($"Archiving project:\n\t{projectFilePath}...");

            logger.LogInformation("Checking if project is a library...");

            var isLibrary = Instances.ProjectFileOperator.IsLibrary_Synchronous(projectFilePath);
            if (isLibrary)
            {
                throw new Exception("This publish operations is not for libraries.");
            }

            // Is executable.
            logger.LogInformation("(Good) Project is an executable, not a library.");

            // Publish to timestamped archive directory for program.
            var publicationBinariesOutputDirectoryPath = PublicationPathsOperator.Instance.GetPublicationBinariesOutputDirectoryPath(
                binariesOutputDirectoryPath,
                projectFilePath);

            var timestampedBinariesDirectoryPath = PublicationPathsOperator.Instance.GetTimestampedBinariesOutputDirectoryPath(publicationBinariesOutputDirectoryPath);

            logger.LogInformation($"Publishing project to directory...\n\tPublish directory:\n\t{timestampedBinariesDirectoryPath}");

            await Instances.PublishOperator.Publish(
                projectFilePath,
                timestampedBinariesDirectoryPath);

            logger.LogInformation($"Publishing project to directory.\n\tPublish directory:\n\t{timestampedBinariesDirectoryPath}");

            // Copy files to current archive directory for program.
            var currentBinariesOutputDirectoryPath = PublicationPathsOperator.Instance.GetCurrentBinariesOutputDirectoryPath(publicationBinariesOutputDirectoryPath);
            var priorBinariesOutputDirectoryPath = PublicationPathsOperator.Instance.GetPriorBinariesOutputDirectoryPath(publicationBinariesOutputDirectoryPath);

            var fileSystemOperator = F0000.Instances.FileSystemOperator;

            var currentDirectoryExists = fileSystemOperator.DirectoryExists(currentBinariesOutputDirectoryPath);
            if (currentDirectoryExists)
            {
                logger.LogInformation($"Deleting prior directory...\n\tPrior directory:\n\t{priorBinariesOutputDirectoryPath}");

                fileSystemOperator.DeleteDirectory_OkIfNotExists(priorBinariesOutputDirectoryPath);

                logger.LogInformation($"Deleted prior directory.\n\tPrior directory:\n\t{priorBinariesOutputDirectoryPath}");

                logger.LogInformation($"Copying current directory to prior directory...\n\tCurrent directory:\n\t{currentBinariesOutputDirectoryPath}\n\tPrior directory:\n\t{priorBinariesOutputDirectoryPath}");

                fileSystemOperator.CopyDirectory(
                    currentBinariesOutputDirectoryPath,
                    priorBinariesOutputDirectoryPath);

                logger.LogInformation($"Copied current directory to prior directory.\n\tCurrent directory:\n\t{currentBinariesOutputDirectoryPath}\n\tPrior directory:\n\t{priorBinariesOutputDirectoryPath}");

                logger.LogInformation($"Deleting current directory...\n\tCurrent directory:\n\t{currentBinariesOutputDirectoryPath}");

                fileSystemOperator.DeleteDirectory_OkIfNotExists(currentBinariesOutputDirectoryPath);

                logger.LogInformation($"Deleted current directory.\n\tCurrent directory:\n\t{currentBinariesOutputDirectoryPath}");
            }

            logger.LogInformation($"Copying timestamped directory to current directory...\n\tTimestamped directory:\n\t{currentBinariesOutputDirectoryPath}\n\tCurrent directory:\n\t{priorBinariesOutputDirectoryPath}");

            fileSystemOperator.CopyDirectory(
                timestampedBinariesDirectoryPath,
                currentBinariesOutputDirectoryPath);

            logger.LogInformation($"Copied timestamped directory to current directory.\n\tTimestamped directory:\n\t{currentBinariesOutputDirectoryPath}\n\tCurrent directory:\n\t{priorBinariesOutputDirectoryPath}");

            return currentBinariesOutputDirectoryPath;
        }
    }
}
