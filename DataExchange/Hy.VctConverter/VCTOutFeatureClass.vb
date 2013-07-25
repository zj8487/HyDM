﻿Imports ESRI.ArcGIS.Geodatabase

Public Class VctOutFeatureClass

#Region "内部变量"

    Private p_FeatureClass As IFeatureClass

    Private p_GeometryType As enumLayerType

    Private p_FeatureClassID As String

    Private p_OutAliasName As String

    Private p_OutName As String

    Private p_OutFields As New List(Of VctField)

    Private p_OutPoints As New List(Of VctPoint)

    Private p_OutLines As New List(Of VctLine)

    Private p_OutPolygons As New List(Of VctPolygon)

    Private p_OutAttributes As New List(Of String)

#End Region

#Region "属性"

    Property FeatureClass() As IFeatureClass
        Get
            Return Me.p_FeatureClass
        End Get
        Set(ByVal value As IFeatureClass)
            Me.p_FeatureClass = value
        End Set
    End Property

    Property GeometryType() As enumLayerType
        Get
            Return Me.p_GeometryType
        End Get
        Set(ByVal value As enumLayerType)
            Me.p_GeometryType = value
        End Set
    End Property

    Property FeatureClassID() As String
        Get
            Return Me.p_FeatureClassID
        End Get
        Set(ByVal value As String)
            Me.p_FeatureClassID = value
        End Set
    End Property

    Property OutAliasName() As String
        Get
            Return Me.p_OutAliasName
        End Get
        Set(ByVal value As String)
            Me.p_OutAliasName = value
        End Set
    End Property

    Property OutName() As String
        Get
            Return Me.p_OutName
        End Get
        Set(ByVal value As String)
            Me.p_OutName = value
        End Set
    End Property

    ReadOnly Property OutFields() As List(Of VctField)
        Get
            Return Me.p_OutFields
        End Get
    End Property

    ReadOnly Property OutPoints() As List(Of VctPoint)
        Get
            Return Me.p_OutPoints
        End Get
    End Property

    ReadOnly Property OutLines() As List(Of VctLine)
        Get
            Return Me.p_OutLines
        End Get
    End Property

    ReadOnly Property OutPolygons() As List(Of VctPolygon)
        Get
            Return Me.p_OutPolygons
        End Get
    End Property

    ReadOnly Property OutAttributes() As List(Of String)
        Get
            Return Me.p_OutAttributes
        End Get
    End Property

#End Region

End Class