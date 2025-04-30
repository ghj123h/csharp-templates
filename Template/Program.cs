// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Text;

string prob = Console.ReadLine();

string name = $"Template{prob}.Solution{prob}";
Type type = Type.GetType(name);
if (type == null) {
    Directory.SetCurrentDirectory("../../..");
    string temp = File.ReadAllText("A.cs");
    File.WriteAllText($"{prob}.cs", temp.Replace("TemplateA", $"Template{prob}").Replace("SolutionA", $"Solution{prob}"));
} else {
    object sol = Activator.CreateInstance(type);
    MethodInfo solve = type.GetMethod("Main");
    solve.Invoke(sol, null);
}