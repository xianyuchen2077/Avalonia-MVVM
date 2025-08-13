using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using System;

namespace GreetingApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        [Reactive]
        public string Name { get; set; } = string.Empty;

        [Reactive]
        public string GreetingMessage { get; set; } = string.Empty;

        public ReactiveCommand<Unit, Unit> GreetCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearCommand { get; }

        public MainWindowViewModel()
        {
            // 创建GreetCommand，当Name不为空且不仅包含空格时可执行
            var canExecuteGreet = this.WhenAnyValue(
                x => x.Name,
                name => !string.IsNullOrWhiteSpace(name));

            GreetCommand = ReactiveCommand.Create(ExecuteGreet, canExecuteGreet);

            // 创建ClearCommand，总是可执行
            ClearCommand = ReactiveCommand.Create(ExecuteClear);
        }

        private void ExecuteGreet()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                GreetingMessage = $"Hello, {Name.Trim()}!";
            }
        }

        private void ExecuteClear()
        {
            Name = string.Empty;
            GreetingMessage = string.Empty;
        }
    }
}