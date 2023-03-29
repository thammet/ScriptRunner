using Moq;
using ScriptRunner.Tests.Data.Scripts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ScriptRunner.Tests
{
    public class ScriptExecutionServiceTests
    {
        private readonly Mock<IScriptRetrievalService> _scriptRetrievalServiceMock;
        private readonly Mock<IDataStore> _dataStoreMock;
        private readonly ScriptExecutionService _scriptExecutionService;

        public ScriptExecutionServiceTests()
        {
             _scriptRetrievalServiceMock = new Mock<IScriptRetrievalService>();
            _dataStoreMock = new Mock<IDataStore>();
            _scriptExecutionService = new ScriptExecutionService(_scriptRetrievalServiceMock.Object, _dataStoreMock.Object);
        }

        [Fact]
        public async Task GetNewScripts()
        {
            var expectedScript1 = new Script1();
            var notExpectedScript2 = new Script2();

            _dataStoreMock.Setup(t => t.GetCompletedScriptNames())
                .ReturnsAsync(new List<string> { notExpectedScript2.GetName() });

            _scriptRetrievalServiceMock.Setup(t => t.Scripts)
                .Returns(new List<IScript> { expectedScript1, notExpectedScript2 });

            var newScripts = await _scriptExecutionService.GetNewScripts();

            Assert.Contains(expectedScript1, newScripts);
            Assert.DoesNotContain(notExpectedScript2, newScripts);
        }

        [Fact]
        public async Task GetNewScripts_Gets_Scripts_With_Same_Names_But_Different_Namespaces()
        {
            var notExpectedScript1 = new Script1();
            var expectedNestedScript1 = new Data.Scripts.Nested.Script1();

            _dataStoreMock.Setup(t => t.GetCompletedScriptNames())
                .ReturnsAsync(new List<string> { notExpectedScript1.GetName() });

            _scriptRetrievalServiceMock.Setup(t => t.Scripts)
                .Returns(new List<IScript> { notExpectedScript1, expectedNestedScript1 });

            var newScripts = await _scriptExecutionService.GetNewScripts();

            Assert.DoesNotContain(notExpectedScript1, newScripts);
            Assert.Contains(expectedNestedScript1, newScripts);
        }

        public async Task ExecuteScripts()
        {
            var scriptMock1 = new Mock<IScript>();
            var scriptMock2 = new Mock<IScript>();

            _dataStoreMock.Setup(t => t.GetCompletedScriptNames())
               .ReturnsAsync(new List<string> {});

            _scriptRetrievalServiceMock.Setup(t => t.Scripts)
                .Returns(new List<IScript> { scriptMock1.Object, scriptMock2.Object });

            await _scriptExecutionService.ExecuteScripts();

            scriptMock1.Verify(t => t.Execute(), Times.Once());
            scriptMock2.Verify(t => t.Execute(), Times.Once());

            _dataStoreMock.Verify(t => t.SetScriptAsCompleted(It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public async Task ExecuteScripts_Stops_On_Exception()
        {
            var scriptMock1 = new Mock<IScript>();
            var scriptMock2 = new Mock<IScript>();
            scriptMock2.Setup(t => t.Execute())
                .ThrowsAsync(new Exception());

            _dataStoreMock.Setup(t => t.GetCompletedScriptNames())
               .ReturnsAsync(new List<string> { });

            _scriptRetrievalServiceMock.Setup(t => t.Scripts)
                .Returns(new List<IScript> { scriptMock1.Object, scriptMock2.Object });

            await Assert.ThrowsAsync<Exception>(() => _scriptExecutionService.ExecuteScripts());

            scriptMock1.Verify(t => t.Execute(), Times.Once());
            scriptMock2.Verify(t => t.Execute(), Times.Once());

            _dataStoreMock.Verify(t => t.SetScriptAsCompleted(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task ExecuteScripts_Handles_When_There_Are_No_Scripts()
        {
            _dataStoreMock.Setup(t => t.GetCompletedScriptNames())
               .ReturnsAsync(new List<string> { });

            _scriptRetrievalServiceMock.Setup(t => t.Scripts)
                .Returns(new List<IScript> { });

            await _scriptExecutionService.ExecuteScripts();

            _dataStoreMock.Verify(t => t.SetScriptAsCompleted(It.IsAny<string>()), Times.Never());
        }
    }
}
