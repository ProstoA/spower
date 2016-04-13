using System;
using System.CodeDom.Compiler;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

using Microsoft.CSharp;

namespace ProstoA.Delivery.Running {
    public class DynamicRunner : IRun {
        private readonly string _code;
        private readonly string[] _usings;
        private readonly string[] _references;

        public DynamicRunner(string code, string[] usings = null, string[] references = null) {
            _code = code;
            _usings = usings;
            _references = references ?? new string[0];
        }

        public void Execute(IRunContext context) {
            try {
                ExecuteInternal(context);
            }
            catch (Exception ex) {
                context.Log("Exec Error: " + ex);
            }
        }

        private void ExecuteInternal(IRunContext context) {
            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters();

            parameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
            parameters.ReferencedAssemblies.AddRange(_references);
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = false;

            var usings = string.Join("", _usings.Select(x => "using " + x + ";\n"));

            var code = usings + @"
namespace ProstoA.Runner {
    public static class _DynamicRunner {
        public static void Run(IRunContext context) {
        " + _code + @"
        }

    }
}";
            var results = provider.CompileAssemblyFromSource(parameters, code);

            var zeroLine = 6 + _usings.Length;

            if (results.Errors.HasErrors) {
                foreach (CompilerError error in results.Errors) {
                    context.Log(string.Format("Error ({0}): {1}. Line: {2}.", error.ErrorNumber, error.ErrorText, error.Line - zeroLine));
                }
                return;
            }

            var assembly = results.CompiledAssembly;
            var program = assembly.GetType("ProstoA.Runner._DynamicRunner");
            var main = program.GetMethod("Run");

            main.Invoke(null, new object[] { context });
        }
    }

    public class ResultContext : IRunContext {
        private readonly Action<string> _log;

        public ResultContext(Action<string> log) {
            Contract.Requires(log != null);
            _log = log;
        }

        public void Log(object obj) {
            _log(obj.ToString());
        }
    }
}