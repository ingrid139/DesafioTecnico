using FluentValidation.Results;

namespace Intituicao.Financeira.Application.Shared.Core
{
    public class Output<T> where T : notnull
    {
        public Output(bool isValid = true) => IsValid = isValid;

        public Output(T result)
        {
            IsValid = true;
            AddResult(result);
        }

        public Output(ValidationResult validationResult)
        {
            ProcessValidationResults(validationResult);
        }

        public Output(IEnumerable<ValidationResult> validationResults)
        {
            ProcessValidationResults(validationResults.ToArray());
        }

        private List<string> _messages;
        private List<string> _errorMessages;
        protected T _result;

        public IReadOnlyCollection<string> ErrorMessages => GetMessages(_errorMessages);
        public bool IsValid { get; protected set; }
        public IReadOnlyCollection<string> Messages => GetMessages(_messages);

        private static IReadOnlyCollection<string> GetMessages(List<string> messages)
        {
            if (messages == null)
            {
                return Array.Empty<string>();
            }
            return messages.AsReadOnly();
        }

        public static void CheckValidationResult(ValidationResult validationResult)
        {
            if (validationResult == null)
            {
                throw new Exception(OutputConstants.ValidationResultNullMessage);
            }
        }

        private static void VerifyErrorMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new Exception(OutputConstants.ErrorMessageIsNullOrEmptyMessage);
            }
        }

        private static void VerifyMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new Exception(OutputConstants.ErrorMessageIsNullOrEmptyMessage);
            }
        }

        private void CreateErrorMessagesWhenThereIsNone()
        {
            _errorMessages ??= new List<string>();
        }

        public void ProcessValidationResults(params ValidationResult[] validationResults)
        {
            foreach (var validationResult in validationResults)
            {
                CheckValidationResult(validationResult);
                AddValidationResult(validationResult);
            }
            VerifyValidity();
        }

        private void VerifyErrorMessages(ValidationResult validationResult)
        {
            CreateErrorMessagesWhenThereIsNone();
            _errorMessages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        protected virtual void VerifyValidity()
        {
            if (ErrorMessages == null)
            {
                IsValid = true;
            }
            else
            {
                IsValid = ErrorMessages.Count == 0;
            }
        }

        public void AddErrorMessage(string message)
        {
            AddErrorMessages(message);
        }

        public void AddErrorMessages(params string[] messages)
        {
            CreateErrorMessagesWhenThereIsNone();
            foreach (var message in messages)
            {
                VerifyErrorMessage(message);
                _errorMessages.Add(message);
            }
            VerifyValidity();
        }

        public void AddMessage(string message) => AddMessages(message);

        public void AddMessages(params string[] messages)
        {
            CreateMessagesWhenThereIsNone();
            foreach (var message in messages)
            {
                VerifyMessage(message);
                _messages.Add(message);
            }
        }

        private void CreateMessagesWhenThereIsNone()
        {
            _messages ??= new List<string>();
        }

        public void AddResult(T result) => _result = result ?? throw new
            Exception(OutputConstants.ResultNullMessage);

        public virtual void AddValidationResult(ValidationResult validationResult)
        {
            CheckValidationResult(validationResult);
            IsValid = validationResult.IsValid;
            VerifyErrorMessages(validationResult);
        }

        public T GetResult() => _result;

    }
    public class Output : Output<object>
    {
        /// <summary>
        /// Cria um Output sem um objeto de retorno
        /// </summary>
        public Output() : base() { }

        /// <summary>
        /// Cria um Output sem um objeto de retorno
        /// </summary>
        public Output(bool isValid = true) : base(isValid) { }

        /// <summary>
        /// Cria um Output com resultado de validações feitas via fail fast validation (FluentValidation)
        /// </summary>
        public Output(ValidationResult validationResult) : base(validationResult) { }

        public Output(IEnumerable<ValidationResult> validationResults) : base(validationResults) { }

        /// <summary>
        /// Cria um Output com o objeto a ser retornado da chamada.
        /// </summary>
        public Output(object result) : base(result) { }

        /// <summary>
        /// Adiciona ao Output um objeto que será devolvido para quem fez a chamada (Result)
        /// </summary>
        public new void AddResult(object result) => _result = result ?? throw new Exception(OutputConstants.ResultNullMessage);
    }
}
