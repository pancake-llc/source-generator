using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace source_generator;

public class SyntaxReceiver : ISyntaxReceiver
{
    public List<(FieldDeclarationSyntax field, AttributeSyntax syntax)> TargetFields { get; } = new List<(FieldDeclarationSyntax field, AttributeSyntax syntax)>();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is FieldDeclarationSyntax {AttributeLists.Count: > 0} field)
        {
            foreach (var list in field.AttributeLists)
            {
                foreach (var attribute in list.Attributes)
                {
                    var nameAttriute = attribute.Name.ToString();
                    if (nameAttriute.EndsWith("AutoPropertyAttribute") || nameAttriute.EndsWith("AutoProperty"))
                    {
                        TargetFields.Add((field, attribute));
                    }
                }
            }
        }
    }
}