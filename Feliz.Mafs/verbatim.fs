namespace Feliz.Mafs

open Fable.Core
open Feliz
open Browser.Types

type Verbatim =
    [<ReactComponent(import="Mafs", from="mafs")>]
    static member Mafs (children: IReactProperty list,
                        ?width  : U2<float, Auto>,
                        ?height : float,
                        ?pan    : bool,
                        ?zoom   : U2<bool, {| min: float; max: float |}>,
                        ?viewBox: ViewBox,
                        ?preserveAspectRatio: U2<bool, Contain>,
                        ?ssr    : bool,
                        ?onClick: Vec2 -> MouseEvent -> unit) = React.imported ()

    [<ReactComponent(import="Point", from="mafs")>]
    static member Point (x              : float,
                         y              : float,
                         ?color         : Color,
                         ?opacity       : float,
                         ?svgCircleProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Vector", from="mafs")>]
    static member Vector (tip          : Vec2,
                          ?tail        : Vec2,
                          ?color       : Color,
                          ?opacity     : float,
                          ?weight      : float,
                          ?style       : LineStyle,
                          ?svgLineProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Circle", from="mafs")>]
    static member Circle (center          : Vec2,
                          radius          : float,
                          ?angle          : float,
                          ?color          : Color,
                          ?weight         : float,
                          ?fillOpacity    : float,
                          ?strokeOpacity  : float,
                          ?strokeStyle    : LineStyle,
                          ?svgEllipseProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Ellipse", from="mafs")>]
    static member Ellipse (center          : Vec2,
                           radius          : Vec2,
                           ?color          : Color,
                           ?weight         : float,
                           ?fillOpacity    : float,
                           ?strokeOpacity  : float,
                           ?strokeStyle    : LineStyle,
                           ?svgEllipseProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Polygon", from="mafs")>]
    static member Polygon (points          : Vec2 array,
                           ?color          : Color,
                           ?weight         : float,
                           ?fillOpacity    : float,
                           ?strokeOpacity  : float,
                           ?strokeStyle    : StrokeStyle,
                           ?svgPolygonProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Polyline", from="mafs")>]
    static member Polyline (points          : Vec2 array,
                            ?color          : Color,
                            ?weight         : float,
                            ?fillOpacity    : float,
                            ?strokeOpacity  : float,
                            ?strokeStyle    : StrokeStyle,
                            ?svgPolygonProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Text", from="mafs")>]
    static member Text (children       : string,
                        x              : float,
                        y              : float,
                        ?attach        : Direction,
                        ?attachDistance: float,
                        ?size          : float,
                        ?color         : Color,
                        ?svgTextProps  : SVGProps) = React.imported ()

    [<ReactComponent(import="LaTeX", from="mafs")>]
    static member LaTeX (tex         : string,
                         at          : Vec2,
                         color       : Color,
                         katexOptions: KatexOptions) = React.imported ()

    [<ReactComponent(import="Transform", from="mafs")>]
    static member Transform (children  : ReactElement list,
                             ?matrix   : Matrix,
                             ?translate: Vec2,
                             ?scale    : U2<float, Vec2>,
                             ?rotate   : float,
                             ?shear    : Vec2) = React.imported ()

    [<Import("useMovablePoint", "mafs")>]
    [<Emit("useMovablePoint($1, {constrain: $2, color: $3})")>]
    static member useMovablePoint (initialPoint: Vec2,
                                   ?constrain  : U2<Constrain, Vec2 -> Vec2>,
                                   ?color      : Color) = jsNative<MovablePoint>

module Verbatim =
    type Coordinates =
        [<ReactComponent(import="Coordinates.Cartesian", from="mafs")>]
        static member Cartesian (?xAxis       : U2<bool, Axis>,
                                 ?yAxis       : U2<bool, Axis>,
                                 ?subdivisions: float) = React.imported ()

        [<ReactComponent(import="Coordinates.Polar", from="mafs")>]
        static member Polar (?xAxis       : U2<bool, Axis>,
                             ?yAxis       : U2<bool, Axis>,
                             ?lines       : float,
                             ?subdivisions: float) = React.imported ()

    type Line =
        [<ReactComponent(import="Line.ThroughPoints", from="mafs")>]
        static member ThroughPoints (point1  : Vec2,
                                     point2  : Vec2,
                                     ?color  : Color,
                                     ?opacity: float,
                                     ?weight : float,
                                     ?style  : LineStyle) = React.imported ()

        [<ReactComponent(import="Line.Segment", from="mafs")>]
        static member LineSegment (point1  : Vec2,
                                   point2  : Vec2,
                                   ?color  : Color,
                                   ?opacity: float,
                                   ?weight : float,
                                   ?style  : LineStyle) = React.imported ()

        [<ReactComponent(import="Line.PointSlope", from="mafs")>]
        static member PointSlope (point   : Vec2,
                                  slope   : float,
                                  ?color  : Color,
                                  ?opacity: float,
                                  ?weight : float,
                                  ?style  : LineStyle) = React.imported ()

        [<ReactComponent(import="Line.PointAngle", from="mafs")>]
        static member PointAngle (point   : Vec2,
                                  angle   : float,
                                  ?color  : Color,
                                  ?opacity: float,
                                  ?weight : float,
                                  ?style  : LineStyle) = React.imported ()

    type Plot =
        [<ReactComponent(import="Plot.OfX", from="mafs")>]
        static member OfX (y                : float -> float,
                           ?color           : Color,
                           ?opacity         : float,
                           ?weight          : float,
                           ?style           : LineStyle,
                           ?minSamplingDepth: float,
                           ?maxSamplingDepth: float) = React.imported ()

        [<ReactComponent(import="Plot.OfY", from="mafs")>]
        static member OfY (x                : float -> float,
                           ?color           : Color,
                           ?opacity         : float,
                           ?weight          : float,
                           ?style           : LineStyle,
                           ?minSamplingDepth: float,
                           ?maxSamplingDepth: float) = React.imported ()

        // x / y -> { ">"?: FnX; "<="?: FnX; "<"?: FnX | undefined; ">="?: FnX | undefined; } | undefined
        [<ReactComponent(import="Plot.Inequality", from="mafs")>]
        static member Inequality (?y                : obj,
                                  ?x                : obj,
                                  ?color            : Color,
                                  ?weight           : float,
                                  ?strokeColor      : Color,
                                  ?strokeOpacity    : float,
                                  ?fillColor        : Color,
                                  ?fillOpacity      : float,
                                  ?minSamplingDepth : float,
                                  ?maxSamplingDepth : float,
                                  ?upperColor       : Color,
                                  ?upperOpacity     : float,
                                  ?upperWeight      : float,
                                  ?lowerColor       : Color,
                                  ?lowerOpacity     : float,
                                  ?lowerWeight      : float,
                                  ?svgUpperPathProps: SVGProps,
                                  ?svgLowerPathProps: SVGProps,
                                  ?svgFillPathProps : SVGProps) = React.imported ()

        [<ReactComponent(import="Plot.Parametric", from="mafs")>]
        static member Parametric (xy               : float -> Vec2,
                                  t                : Vec2,
                                  ?color           : Color,
                                  ?opacity         : float,
                                  ?weight          : float,
                                  ?style           : LineStyle,
                                  ?minSamplingDepth: float,
                                  ?maxSamplingDepth: float,
                                  ?svgPathProps    : SVGProps) = React.imported ()

        [<ReactComponent(import="Plot.VectorField", from="mafs")>]
        static member VectorField (xy          : Vec2 -> Vec2,
                                   ?xyOpacity  : Vec2 -> float,
                                   ?step       : float,
                                   ?opacityStep: float,
                                   ?color      : Color) = React.imported ()

    type Debug =
        [<ReactComponent(import="Debug.TransformWidget", from="mafs")>]
        static member DebugTransformWidget (children: ReactElement list) = React.imported ()

        [<ReactComponent(import="Debug.ViewportInfo", from="mafs")>]
        static member DebugViewPortInfo (precision: int) = React.imported ()
