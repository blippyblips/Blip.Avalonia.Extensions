using System;

namespace AutoEditor;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class TabGroupAttribute (string groupName = "Group") : Attribute {
  public string GroupName { get; } = groupName;
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class GroupAttribute (string groupName = "Group") : Attribute
{
  public string GroupName { get; } = groupName;
}

public enum CollectionType { Single, Multiple, Grid, LargeGrid };

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class DrawAsCollectionAttribute (CollectionType collectionType, Type elementType) : Attribute {
  public CollectionType CollectionType { get; } = collectionType;
  public Type ElementType { get; } = elementType;
}

[AttributeUsage(AttributeTargets.Class)]
public class TypeDrawer (params Type[] supportedTypes) : Attribute {
  public Type[] Types { get; } = supportedTypes;
}
