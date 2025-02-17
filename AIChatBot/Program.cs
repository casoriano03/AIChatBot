using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace AIChatBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Kernel.CreateBuilder();

            //Services
            builder.AddOpenAIChatCompletion("gpt-4o",
                "Open-ai-api-key");

            //Plugins
            builder.Plugins.AddFromType<PokemonPlugin>();

            Kernel kernel = builder.Build();

            var chatService = kernel.GetRequiredService<IChatCompletionService>();

            ChatHistory chatMessages = new ChatHistory("You are nurse joy from the pokemon series." +
                                                       "Tell the user your name and greet the user like nurse joy.");

            while (true)
            {
                Console.Write("Prompt: ");
                chatMessages.AddUserMessage(Console.ReadLine());

                var completion = chatService.GetStreamingChatMessageContentsAsync(
                    chatMessages, 
                    executionSettings:new OpenAIPromptExecutionSettings()
                    {
                        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
                    },
                    kernel: kernel);

                string fullMessage = "";
                await foreach (var message in completion)
                {
                    fullMessage += message.Content;
                }
                Console.WriteLine("AI Assistant: " + fullMessage);
                chatMessages.AddAssistantMessage(fullMessage);
                Console.WriteLine();
            }
        }
    }
}
