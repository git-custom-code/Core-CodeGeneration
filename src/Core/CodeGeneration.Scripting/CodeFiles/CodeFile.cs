namespace CustomCode.Core.CodeGeneration.Scripting.CodeFiles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Standard implementation of the <see cref="ICodeFile"/> interface.
    /// </summary>
    public sealed class CodeFile : ICodeFile
    {
        #region Data

        /// <inheritdoc />
        public Identity<string> Id { get; }

        /// <summary>
        /// Gets the internal <see cref="StringBuilder"/> that is used to generate the code file's code.
        /// </summary>
        private StringBuilder CodeBuilder { get; } = new StringBuilder();

        #endregion

        #region Logic

        /// <inheritdoc />
        public string GenerateCode()
        {
            var code = CodeBuilder.ToString();
            var codeLines = code.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var unwantedIndent = GetUnwantedScriptCodeIndent(code);

            var result = new StringBuilder();
            foreach (var line in codeLines)
            {
                if (line.Length > unwantedIndent)
                {
                    result.AppendLine(line.Substring((int)unwantedIndent));
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    result.AppendLine();
                }
                else
                {
                    result.AppendLine("?");
                }
            }
            return result.ToString().Trim();
        }

        /// <summary>
        /// Try to find the unwanted indent of the code file's source code.
        /// </summary>
        /// <param name="code"> The original source code from the script. </param>
        /// <returns> The unwanted indentation that can be removed. </returns>
        private uint GetUnwantedScriptCodeIndent(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return 0u;
            }

            var unwantedIndent = uint.MaxValue;
            var currentLineNonWhitespaceFound = false;
            var currentLineIndent = 0u;

            foreach(var @char in code)
            {
                if (currentLineNonWhitespaceFound == false)
                {
                    if (char.IsWhiteSpace(@char))
                    {
                        ++currentLineIndent;
                    }
                    else
                    {
                        currentLineNonWhitespaceFound = true;
                    }
                }

                if (@char == '\n')
                {
                    if (currentLineNonWhitespaceFound && unwantedIndent > currentLineIndent)
                    {
                        unwantedIndent = currentLineIndent;
                    }
                    currentLineIndent = 0u;
                    currentLineNonWhitespaceFound = false;
                }
            }

            return unwantedIndent;
        }

        /// <inheritdoc />
        public ICodeFile EmitOnce(string sourceCode)
        {
            CodeBuilder.AppendLine(sourceCode);
            return this;
        }

        /// <inheritdoc />
        public ICodeFile EmitForEach<T>(IEnumerable<T> array, Func<T, string> createSourceCodeAction)
        {
            foreach (var item in array)
            {
                CodeBuilder.AppendLine(createSourceCodeAction(item));
            }
            return this;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Id} {GenerateCode()}";
        }

        #endregion
    }
}