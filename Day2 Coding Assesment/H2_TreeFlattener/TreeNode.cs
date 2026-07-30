using System.Collections.Generic;

class TreeNode
{
    public string Value;
    public List<TreeNode> Children;

    public TreeNode(string val)
    {
        Value = val;
        Children = new List<TreeNode>();
    }

    public void AddChild(TreeNode child)
    {
        Children.Add(child);
    }
}
