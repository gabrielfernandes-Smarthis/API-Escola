namespace SmartSchool.WebAPI.Helpers;

public static class DateTimeExtensions
{
    public static int PegarIdade(this DateTime dateTime)
    {
        DateTime dataAtual = DateTime.UtcNow;
        int idade = dataAtual.Year - dateTime.Year;

        if (dataAtual < dateTime.AddYears(idade))
            idade--;
        return idade;
    }
}