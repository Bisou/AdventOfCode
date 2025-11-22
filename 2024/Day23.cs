using System.Text;

public class Day23
{
    private const int day = 23;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);  
        var graph = new Dictionary<string, List<string>>();                
        foreach(var line in input) {
            var data = line.Split('-');
            if (graph.TryGetValue(data[0], out var list)) {
                list.Add(data[1]);
            } else {
                graph[data[0]] = new List<string>{data[1]};
            }
            if (graph.TryGetValue(data[1], out list)) {
                list.Add(data[0]);
            } else {
                graph[data[1]] = new List<string>{data[0]};
            }
        }
        
        var total=0L; 
        var seen = new HashSet<(string C1, string C2, string C3)>();
        foreach(var c1 in graph.Keys) {
            if (c1[0]!='t') continue;            
            foreach(var c2 in graph[c1]) {
                foreach(var c3 in graph[c2]) {
                    if (graph[c1].Contains(c3)) {
                        var trio = new [] {c1,c2,c3};
                        Array.Sort(trio);
                        var key = (trio[0], trio[1], trio[2]);
                        if (seen.Contains(key)) continue;
                        seen.Add(key);
                        total++;
                    }
                }
            }
        }
        Console.WriteLine($"Part1: result is {total}");
    }

    public static void SolvePart2(string dataType)
    {
        Tool.LogStart(day,2,dataType);        
        var input = Tool.ReadAll(day, dataType);  

        var graph = new Dictionary<string, HashSet<string>>();                
        foreach(var line in input) {
            var data = line.Split('-');
            if (graph.TryGetValue(data[0], out var list)) {
                list.Add(data[1]);
            } else {
                graph[data[0]] = new HashSet<string>{data[1]};
            }
            if (graph.TryGetValue(data[1], out list)) {
                list.Add(data[0]);
            } else {
                graph[data[1]] = new HashSet<string>{data[0]};
            }
        }
        

        //var ssc = GetScc(graph);
        BronKerbosch.FindCliques(graph);
        Console.WriteLine($"Part2: password is {string.Join(",", BronKerbosch.BestClique.OrderBy(x => x))}");
    }
}
  
public static class BronKerbosch
{
    public static HashSet<string> BestClique;

    // Méthode principale pour trouver les cliques maximales
    public static void FindCliques(Dictionary<string, HashSet<string>> graph)
    {
        BestClique = new HashSet<string>();
        var R = new HashSet<string>();
        var P = new HashSet<string>(graph.Keys);
        var X = new HashSet<string>();

        BronKerboschRecursive(graph, R, P, X);
    }

    // Méthode récursive de Bron-Kerbosch
    private static void BronKerboschRecursive(Dictionary<string, HashSet<string>> graph, HashSet<string> R, HashSet<string> P, HashSet<string> X)
    {
        if (!P.Any() && !X.Any()) // Aucun candidat restant, clique maximale trouvée
        {            
            if (R.Count>BestClique.Count)
                BestClique = new HashSet<string>(R);
            //Console.WriteLine("Clique maximale: " + string.Join(", ", R.OrderBy(c => c)));

            return;
        }

        // Copier P pour éviter des modifications pendant l'itération
        var PClone = new HashSet<string>(P);

        foreach (var v in PClone)
        {
            R.Add(v); // Ajouter v à la clique courante
            var neighbors = graph[v];
            BronKerboschRecursive(
                graph,
                new HashSet<string>(R),
                new HashSet<string>(P.Intersect(neighbors)),
                new HashSet<string>(X.Intersect(neighbors))
            );
            R.Remove(v); // Retirer v pour continuer avec d'autres candidats
            P.Remove(v); // Exclure v des candidats restants
            X.Add(v); // Ajouter v aux exclus
        }
    }

}
