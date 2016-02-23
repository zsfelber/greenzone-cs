using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GreenZoneParser.Parsers.Cs;

namespace GreenZoneParser.Parsers
{
    public enum StatementId
    {
        Unknown,

        Break,
        Continue,
        Catch,
        Do,
        Else,
        Finally,
        Fixed,
        ForEach,
        For,
        If,
        Lock,
        Return,
        Switch,
        Throw,
        Try,
        UsingBlock,
        UsingDecl,
        While,
        CheckedStatement,
        UncheckedStatement,

        ForEachSub,
        For1or3Sub,

        ClassDef,
        DelegateDef,
        EventDecl,
        EnumDef,
        InterfaceDef,
        NamespaceDef,
        StructDef,

        ArrayInitializer,
        Case,
        Empty,
        EnumConstant,
        FieldDecl,
        MethodDef,
        IndexerDef,
        ConstructorDef,
        DestructorDef,
        PropertyDef,
        PropertyGet,
        PropertySet,
        VarDecl,

        AtomicExpression,
        Argument,
        ArrayAccess,
        ArrayCreation,
        BinaryOperation,
        BinaryShiftOperation,
        Cast,
        Checked,
        DefaultExpression,
        FieldAccessOrId,
        MethodExpression,
        ObjectCreation,
        Paren,
        SystemType,
        TernaryOperation,
        Typeof,
        UnaryOperation,
        Unchecked,
        UserType,
        VarDeclArgument,

        Linq,
        LinqContinuation,
        LinqFrom,
        LinqGroupBy,
        LinqJoin,
        LinqLet,
        LinqOrderBy,
        LinqOrdering,
        LinqSelect,
        LinqWhere
    }

    public static class StatementIdEx
    {

    }

}
