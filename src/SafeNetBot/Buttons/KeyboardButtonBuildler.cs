using Telegram.Bot.Types.ReplyMarkups;

namespace SafeNetBot.Buttons;

public class ReplyKeyboardMarkupBuilder
{
    private readonly List<KeyboardButton[]> _buttons = new List<KeyboardButton[]>();
    private bool _resizeKeyboard = false;

    public ReplyKeyboardMarkupBuilder AddRow(params string[] buttonTexts)
    {
        var row = new KeyboardButton[buttonTexts.Length];
        for (int i = 0; i < buttonTexts.Length; i++)
        {
            row[i] = new KeyboardButton(buttonTexts[i]);
        }
        _buttons.Add(row);
        return this;
    }

    public ReplyKeyboardMarkupBuilder SetResizeKeyboard(bool resize)
    {
        _resizeKeyboard = resize;
        return this;
    }

    public ReplyKeyboardMarkup Build()
    {
        return new ReplyKeyboardMarkup(_buttons.ToArray())
        {
            ResizeKeyboard = _resizeKeyboard
        };
    }
}
