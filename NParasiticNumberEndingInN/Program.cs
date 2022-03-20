using NParasiticNumberEndingInN;

ParasiticNumberFinder finder = new(2, 10);
Console.WriteLine(finder.Calculate());

finder = new(4, 16);
Console.WriteLine(finder.Calculate());

finder = new(3, 16);
Console.WriteLine(finder.Calculate());