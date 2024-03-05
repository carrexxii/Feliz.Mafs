namespace Feliz.Mafs

open Fable.Core
open Feliz
open Browser.Types

type VerbatimMovablePoint =
    { element : ReactElement
      point   : float array
      setPoint: float array -> unit
      x       : float
      y       : float }

type Verbatim =
    [<ReactComponent(import="Mafs", from="mafs")>]
    static member Mafs (children: IReactProperty list,
                        ?width  : U2<float, Auto>,
                        ?height : float,
                        ?pan    : bool,
                        ?zoom   : U2<bool, {| min: float; max: float |}>,
                        ?viewBox: {| x: float array; y: float array; padding: float |},
                        ?preserveAspectRatio: U2<bool, Contain>,
                        ?ssr    : bool,
                        ?onClick: float array -> MouseEvent -> unit) = React.imported ()

    [<ReactComponent(import="Point", from="mafs")>]
    static member Point (x              : float,
                         y              : float,
                         ?color         : Color,
                         ?opacity       : float,
                         ?svgCircleProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Vector", from="mafs")>]
    static member Vector (tip          : float array,
                          ?tail        : float array,
                          ?color       : Color,
                          ?opacity     : float,
                          ?weight      : float,
                          ?style       : LineStyle,
                          ?svgLineProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Circle", from="mafs")>]
    static member Circle (center          : float array,
                          radius          : float,
                          ?angle          : float,
                          ?color          : Color,
                          ?weight         : float,
                          ?fillOpacity    : float,
                          ?strokeOpacity  : float,
                          ?strokeStyle    : LineStyle,
                          ?svgEllipseProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Ellipse", from="mafs")>]
    static member Ellipse (center          : float array,
                           radius          : float array,
                           ?color          : Color,
                           ?weight         : float,
                           ?fillOpacity    : float,
                           ?strokeOpacity  : float,
                           ?strokeStyle    : LineStyle,
                           ?svgEllipseProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Polygon", from="mafs")>]
    static member Polygon (points          : float array array,
                           ?color          : Color,
                           ?weight         : float,
                           ?fillOpacity    : float,
                           ?strokeOpacity  : float,
                           ?strokeStyle    : LineStyle,
                           ?svgPolygonProps: SVGProps) = React.imported ()

    [<ReactComponent(import="Polyline", from="mafs")>]
    static member Polyline (points          : float array array,
                            ?color          : Color,
                            ?weight         : float,
                            ?fillOpacity    : float,
                            ?strokeOpacity  : float,
                            ?strokeStyle    : LineStyle,
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
    static member LaTeX (tex          : string,
                         ?at          : float array,
                         ?color       : Color,
                         ?katexOptions: KatexOptions) = React.imported ()

    [<ReactComponent(import="Transform", from="mafs")>]
    static member Transform (children  : ReactElement list,
                             ?matrix   : Matrix,
                             ?translate: float array,
                             ?scale    : U2<float, float array>,
                             ?rotate   : float,
                             ?shear    : float array) = React.imported ()

    [<Import("useMovablePoint", "mafs")>]
    [<Emit("useMovablePoint($1, {constrain: $2, color: $3})")>]
    static member useMovablePoint (initialPoint: float array,
                                   ?constrain  : U2<string, float array -> float array>,
                                   ?color      : Color) = jsNative<VerbatimMovablePoint>

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
        static member ThroughPoints (point1  : float array,
                                     point2  : float array,
                                     ?color  : Color,
                                     ?opacity: float,
                                     ?weight : float,
                                     ?style  : LineStyle) = React.imported ()

        [<ReactComponent(import="Line.Segment", from="mafs")>]
        static member LineSegment (point1  : float array,
                                   point2  : float array,
                                   ?color  : Color,
                                   ?opacity: float,
                                   ?weight : float,
                                   ?style  : LineStyle) = React.imported ()

        [<ReactComponent(import="Line.PointSlope", from="mafs")>]
        static member PointSlope (point   : float array,
                                  slope   : float,
                                  ?color  : Color,
                                  ?opacity: float,
                                  ?weight : float,
                                  ?style  : LineStyle) = React.imported ()

        [<ReactComponent(import="Line.PointAngle", from="mafs")>]
        static member PointAngle (point   : float array,
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
        static member Parametric (xy               : float -> float array,
                                  t                : float array,
                                  ?color           : Color,
                                  ?opacity         : float,
                                  ?weight          : float,
                                  ?style           : LineStyle,
                                  ?minSamplingDepth: float,
                                  ?maxSamplingDepth: float,
                                  ?svgPathProps    : SVGProps) = React.imported ()

        [<ReactComponent(import="Plot.VectorField", from="mafs")>]
        static member VectorField (xy          : float array -> float array,
                                   ?step       : float,
                                   ?xyOpacity  : float array -> float,
                                   ?opacityStep: float,
                                   ?color      : Color) = React.imported ()

    type vec =
        [<Import("vec.add"        , "mafs")>] static member add         (v: float array, v2: float array)           = jsNative<float array>
        [<Import("vec.sub"        , "mafs")>] static member sub         (v: float array, v2: float array)           = jsNative<float array>
        [<Import("vec.scale"      , "mafs")>] static member scale       (v: float array, sc: float)                 = jsNative<float array>
        [<Import("vec.dist"       , "mafs")>] static member dist        (v: float array, v2: float array)           = jsNative<float>
        [<Import("vec.mag"        , "mafs")>] static member mag         (v: float array)                            = jsNative<float>
        [<Import("vec.dot"        , "mafs")>] static member dot         (v: float array, v2: float array)           = jsNative<float>
        [<Import("vec.lerp"       , "mafs")>] static member lerp        (v: float array, v2: float array, t: float) = jsNative<float array>
        [<Import("vec.midpoint"   , "mafs")>] static member midpoint    (v: float array, v2: float array)           = jsNative<float array>
        [<Import("vec.normal"     , "mafs")>] static member normal      (v: float array)                            = jsNative<float array>
        [<Import("vec.normalize"  , "mafs")>] static member normalize   (v: float array)                            = jsNative<float array>
        [<Import("vec.rotate"     , "mafs")>] static member rotate      (v: float array, a: float)                  = jsNative<float array>
        [<Import("vec.rotateAbout", "mafs")>] static member rotateAbout (v: float array, cp: float array, a: float) = jsNative<float array>
        [<Import("vec.squareDist" , "mafs")>] static member squareDist  (v: float array, v2: float array)           = jsNative<float array>
        [<Import("vec.withMag"    , "mafs")>] static member withMag     (v: float array, m: float)                  = jsNative<float array>

        [<Import("vec.matrixBuilder", "mafs")>] static member matrixBuilder (?m: Matrix)                = jsNative<IMatrixBuilder>
        [<Import("vec.det"          , "mafs")>] static member det           (m: Matrix)                 = jsNative<float>
        [<Import("vec.matrixInvert" , "mafs")>] static member matrixInvert  (a: Matrix)                 = jsNative<Matrix option>
        [<Import("vec.matrixMult"   , "mafs")>] static member matrixMult    (m: Matrix, m2: Matrix)     = jsNative<Matrix>
        [<Import("vec.toCSS"        , "mafs")>] static member toCSS         (matrix: Matrix)            = jsNative<string>
        [<Import("vec.transform"    , "mafs")>] static member transform     (v: float array, m: Matrix) = jsNative<float array>

    type Debug =
        [<ReactComponent(import="Debug.TransformWidget", from="mafs")>]
        static member DebugTransformWidget (children: ReactElement list) = React.imported ()

        [<ReactComponent(import="Debug.ViewportInfo", from="mafs")>]
        static member DebugViewPortInfo (precision: int) = React.imported ()

    type Katex =
        [<Import("default.render", "katex")>]
        static member render (text    : string,
                              element : HTMLElement,
                              ?options: KatexOptions) = jsNative<unit>

        [<Import("default.renderToString", "katex")>]
        static member renderToString (text    : string,
                                      ?options: KatexOptions) = jsNative<string>
