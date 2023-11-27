using System.Text;

string? Compress(string? str)
{
    // если строка пуста или null или её длина равна 1 - возвращаем саму строку
    if (string.IsNullOrEmpty(str) || str.Length == 1) return str;
    var result = new StringBuilder();
    //поскольку будем сравнивать текущую букву и следующую за ней, то текущая буква встретилась 1 раз
    int count = 1;
    for (int i = 0; i < str.Length - 1; ++i)
    {
        //если следующая буква неравна текущей, то компрессируем последовательность букв
        if (str[i] != str[i + 1])
        {
            result.Append(str[i] + (count == 1 ? "" : count.ToString()));
            count = 0;
        }
        //+1 к количеству текущей буквы или =1 для новой буквы
        ++count;
    }
    //для последней буквы
    result.Append(str[^1] + (count == 1 ? "" : count.ToString()));
    return result.ToString();
}

string? Decompress(string? str)
{
    // если строка пуста или null или её длина равна 1 - возвращаем саму строку
    if (string.IsNullOrEmpty(str) || str.Length == 1) return str;
    var result = new StringBuilder();
    for (int i = 0; i < str.Length; i++)
    {
        //если текущий символ - буква, то добавляем в декомпрессируемую строку
        if (char.IsLetter(str[i]))
            result.Append(str[i]);
        //если текущий символ - цифра, то собираем число,
        //которое является количеством повторений последней встреченной буквы
        else
        {
            var stringRepeatLetter = new StringBuilder(str[i].ToString());
            int j = i + 1;
            while (j < str.Length && char.IsDigit(str[j]))
                stringRepeatLetter.Append(str[j++]);
            result.Append(str[i - 1], repeatCount: int.Parse(stringRepeatLetter.ToString()) - 1);
            i = j - 1;
        }
    }
    return result.ToString();
}

int[] ToSpiralOrder(int[,] matrix)
{
    Directions direction = Directions.DOWN;
    var rows = matrix.GetLength(0);
    var cols = matrix.GetLength(1);
    int[] result = new int[rows * cols];
    //границы, при наступлении которых меняется направление
    int bottomLevel = rows;
    int topLevel = -1;
    int rightLevel = cols;
    int leftLevel = 0;
    for (int i = 0, j = 0, k = 0; k < rows * cols; k++)
    {
        switch (direction)
        {
            case Directions.DOWN:
                result[k] = matrix[i++, j];
                //достигли низа - меняем направление вправо
                if (i == bottomLevel)
                {
                    direction = Directions.RIGHT;
                    i--;
                    j++;
                }
                break;
            case Directions.RIGHT:
                result[k] = matrix[i, j++];
                //достигли правой границы - идем вверх
                if (j == rightLevel)
                {
                    direction = Directions.UP;
                    j--;
                    i--;
                }
                break;
            case Directions.UP:
                result[k] = matrix[i--, j];
                //достигли верха - идем влево
                if (i == topLevel)
                {
                    direction = Directions.LEFT;
                    i++;
                    j--;
                }
                break;
            case Directions.LEFT:
                result[k] = matrix[i, j--];
                //достигли левой границы - идём вниз
                if (j == leftLevel)
                {
                    direction = Directions.DOWN;
                    j++;
                    i++;
                    //обновляем границы на новый виток спирали
                    ++topLevel;
                    --bottomLevel;
                    --rightLevel;
                    ++leftLevel;
                }
                break;
        }
    }

    return result;
}

enum Directions
{
    DOWN,
    RIGHT,
    UP,
    LEFT
}