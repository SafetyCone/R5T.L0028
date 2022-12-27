using System;

using R5T.T0132;


namespace R5T.L0028
{
    [FunctionalityMarker]
    public partial interface IPublicationPathsOperator : IFunctionalityMarker
    {
        public string GetPriorBinariesOutputDirectoryPath(
            string publicationBinariesOutputDirectoryPath)
        {
            var priorBinariesOutputDirectoryPath = Instances.PathOperator.GetDirectoryPath(
                publicationBinariesOutputDirectoryPath,
                DirectoryNames.Instance.Prior);

            return priorBinariesOutputDirectoryPath;
        }

        public string GetCurrentBinariesOutputDirectoryPath(
            string publicationBinariesOutputDirectoryPath)
        {
            var currentBinariesOutputDirectoryPath = Instances.PathOperator.GetDirectoryPath(
                publicationBinariesOutputDirectoryPath,
                DirectoryNames.Instance.Current);

            return currentBinariesOutputDirectoryPath;
        }

        public string GetCurrentBinariesOutputDirectoryPath(
            string binariesOutputDirectoryPath,
            string projectFilePath)
        {
            var publicationBinariesOutputDirectoryPath = this.GetPublicationBinariesOutputDirectoryPath(
                binariesOutputDirectoryPath,
                projectFilePath);

            var currentBinariesOutputDirectoryPath = Instances.PathOperator.GetDirectoryPath(
                publicationBinariesOutputDirectoryPath,
                DirectoryNames.Instance.Current);

            return currentBinariesOutputDirectoryPath;
        }

        public string GetPublicationBinariesOutputDirectoryPath(
            string binariesOutputDirectoryPath,
            string projectFilePath)
        {
            var projectName = F0040.F000.ProjectPathsOperator.Instance.GetProjectName(projectFilePath);

            var publicationName = PublicationNameOperator.Instance.GetPublicationName(projectName);

            var publicationDirectoryName = this.GetPublicationDirectoryName(publicationName);

            var publicationBinariesOutputDirectoryPath = Instances.PathOperator.GetDirectoryPath(
                binariesOutputDirectoryPath,
                publicationDirectoryName);

            return publicationBinariesOutputDirectoryPath;
        }

        public string GetPublicationDirectoryName(string publicationName)
        {
            // Just use the publication name, assuming the publication name can be a directory name.
            var publicationDirectoryName = publicationName;
            return publicationDirectoryName;
        }

        public string GetTimestampedBinariesOutputDirectoryPath(
            string publicationBinariesOutputDirectoryPath)
        {
            var nowLocal = F0000.Instances.NowOperator.GetNow_Local();

            var yyyymmdd_hhmmss = F0000.Instances.DateTimeOperator.ToString_YYYYMMDD_HHMMSS(nowLocal);

            // Just use the yyymmdd_hhmmss value since it can be a directory name.
            var timestampedDirectoryName = yyyymmdd_hhmmss;

            var timestampedBinariesOutputDirectoryPath = Instances.PathOperator.GetDirectoryPath(
                publicationBinariesOutputDirectoryPath,
                timestampedDirectoryName);

            return timestampedBinariesOutputDirectoryPath;
        }
    }
}
