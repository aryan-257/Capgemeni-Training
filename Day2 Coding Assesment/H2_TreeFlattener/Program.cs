using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // root1 bnaya
        var root1 = new TreeNode("A");
        root1.AddChild(new TreeNode("A1"));
        root1.AddChild(new TreeNode("A2"));

        // root2 - nested h thoda
        var root2 = new TreeNode("B");
        var b1 = new TreeNode("B1");
        b1.AddChild(new TreeNode("B1a"));
        b1.AddChild(new TreeNode("B1b"));
        root2.AddChild(b1);

        var root3 = new TreeNode("C");

        List<string> flattened = TreeFlattenerHelper.FlattenTree(root1, root2, root3);

        Console.WriteLine();
        Console.WriteLine("Flattened List: " + string.Join(", ", flattened));
    }
}
