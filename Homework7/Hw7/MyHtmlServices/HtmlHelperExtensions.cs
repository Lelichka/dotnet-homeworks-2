using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
    {
        var content = new HtmlContentBuilder();
        var model = helper.ViewData.Model;
        var modelType = helper.ViewData.ModelMetadata.ModelType;
        
        foreach (var property in modelType.GetProperties())
        {
            content.AppendHtmlLine("<div>");
            var displayAttr = property.GetCustomAttribute<DisplayAttribute>();
            var label = (displayAttr == null)
                ? String.Join(' ', Regex.Split(property.Name, "(?=\\p{Lu})").Where(str => str != "").ToArray())
                : displayAttr.Name;
            content.AppendHtmlLine($"<label for=\"{property.Name}\">{label}</label><br>");
            CreateInputField(property, ref content, model);
            if (model != null)
                Validate(property,ref content,model);
            content.AppendHtmlLine("</div>");
        }
        return content;
    }

    private static void CreateInputField(PropertyInfo property, ref HtmlContentBuilder content,object? model)
    {
        var type = property.PropertyType;
        var value = (model != null)?$"\"{property.GetValue(model)}\"":"";
        if (type.IsEnum)
        {
            content.AppendHtmlLine($"<select id=\"{property.Name}\" name=\"{property.Name}\" value={value}/>");
            foreach (var enumValue in type.GetEnumValues())
                content.AppendHtmlLine($"<option>{enumValue}</option>");
            content.AppendHtmlLine("</select>");
        }
        else if (type == typeof(int))
        {
            content.AppendHtmlLine($"<input id=\"{property.Name}\" name=\"{property.Name}\" type=\"number\" value={value}>");
        }
        else
            content.AppendHtmlLine($"<input id=\"{property.Name}\" name=\"{property.Name}\" type=\"text\" value={value}>");
    }
    private static void Validate(PropertyInfo property, ref HtmlContentBuilder content,object model)
    {
        var validateAttributes = property.GetCustomAttributes<ValidationAttribute>();
        foreach (var attr in validateAttributes)
        {
            if (!attr.IsValid(property.GetValue(model)))
                content.AppendHtmlLine($"<span>{attr.ErrorMessage}</span>");
        }
    }
} 


