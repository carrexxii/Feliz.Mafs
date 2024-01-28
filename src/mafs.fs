module SharpMafs

open Feliz

// TODO: convert float list -> vec.Vector2
[<AutoOpen>]
type Mafs =
    [<ReactComponent(import="Mafs", from="mafs")>]
    static member Mafs (children: IReactProperty list,
                        ?width  : string,
                        ?height : int,
                        ?pan    : bool,
                        ?zoom   : bool,
                        ?viewBox: {| x: int list
                                     y: int list |},
                        ?preserveAspectRation: string,
                        ?ssr    : bool,
                        ?onClick: unit -> int) = React.imported ()

    [<ReactComponent(import="Coordinates.Cartesian", from="mafs")>]
    static member Cartesian (xAxis  : bool,
                             yAxis  : bool,
                             ?subdiv: bool) = React.imported ()
