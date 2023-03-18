using System.Collections.Generic;
using DevelopServer.Parse;

namespace DevelopServer;

public class BaseModuleGenerator : IModuleGenerator
{
    private IDictionary<string, object> _model;

    public BaseModuleGenerator(IDictionary<string, object> model)
    {
        _model = model;
    }

    public virtual string Generate(string source)
    {
        var parser = new Parser(source);
        return parser.Evaluate(_model);
    }
}