using System.IO;
using TestStack.White.UITests.Infrastructure;
using White.Core;
using White.Core.Factory;
using White.Core.UIItems;
using White.Core.UIItems.WindowItems;
using White.Repository.Services;
using White.Repository.Sessions;
using Xunit;

namespace White.Repository.UITests.Sessions
{
    public class WorkSessionTest
    {
        [Fact]
        public void ShouldNotSaveAnyWindowInformationToFileWhenNoWindowsAreLaunched()
        {
            int numberOfFilesBeforeSessionStart = NumberOfFiles();
            using (WorkSession()){}
            Assert.Equal(numberOfFilesBeforeSessionStart, NumberOfFiles());
        }

        [Fact]
        public void ShouldSaveWindowInformationInFile()
        {
            File.Delete("foo.xml");
            using (WorkSession workSession = WorkSession())
            {
                Application application = new WinformsTestConfiguration().LaunchApplication();
                workSession.Attach(application);
                Window window = application.GetWindow("Form1", InitializeOption.NoCache.AndIdentifiedBy("foo"));
                window.Get<Button>("buton");
            }
            Assert.Equal(true, File.Exists("foo.xml"));
        }

        private static WorkSession WorkSession()
        {
            return new WorkSession(new WorkConfiguration(), new NullWorkEnvironment());
        }

        [Fact]
        public void ShouldFindControlBasedLocation()
        {
            File.Delete("foo.xml");
            using (WorkSession workSession = WorkSession())
            {
                Application application = new WinformsTestConfiguration().LaunchApplication();
                workSession.Attach(application);
                Window window = application.GetWindow("Form1", InitializeOption.NoCache.AndIdentifiedBy("foo"));
                window.Get<Button>("buton");
                window.Get<Button>("addNode");
            }
            using (WorkSession workSession = WorkSession())
            {
                Application application = new WinformsTestConfiguration().LaunchApplication();
                workSession.Attach(application);
                Window window = application.GetWindow("Form1", InitializeOption.NoCache.AndIdentifiedBy("foo"));
                window.Get<Button>("buton");
                window.Get<Button>("addNode");
            }
        }

        private static int NumberOfFiles()
        {
            return Directory.GetFiles(".").Length;
        }
    }
}