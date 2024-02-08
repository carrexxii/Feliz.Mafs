namespace Feliz.Mafs

open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open Feliz

module Mafs =
    type Props =
        { width     : U2<float, Auto>
          height    : float
          pan       : bool
          zoom      : U2<bool, {| min: float; max: float |}>
          viewBox   : {| x: Vec2; y: Vec2; padding: float |}
          preserveAR: U2<bool, Contain>
          ssr       : bool
          onClick   : float array -> MouseEvent -> unit }
        static member Default =
            { width      = !^Auto
              height     = 500
              pan        = true
              zoom       = !^false
              viewBox    = {| x = vec -3 3; y = vec -3 3; padding = 0.5 |}
              onClick    = fun _ _ -> ()
              ssr        = false
              preserveAR = !^Contain }
        static member DefaultSquare =
            { Props.Default with width = !^500.0 }

    let create () = Props.Default
    let width      (width: float)     props = { props with width      = !^width             }
    let height     height             props = { props with height     = height              }
    let pan        pan                props = { props with pan        = pan                 }
    let zoom       zoomX zoomY        props = { props with zoom       = !^{| min = zoomX
                                                                             max = zoomY |} }
    let viewBox    viewBox            props = { props with viewBox    = viewBox             }
    let preserveAR (preserveAR: bool) props = { props with preserveAR = !^preserveAR        }
    let onClick    onClick            props = { props with onClick    = onClick             }

    let Mafs (props: Props) (children: ReactElement list) =
        let viewBox = {| x = props.viewBox.x.array; y = props.viewBox.y.array; padding = props.viewBox.padding |}
        Verbatim.Mafs (children = (prop.key 1)::[prop.children children], width = props.width, height = props.height,
                       pan = props.pan, zoom = props.zoom, viewBox = viewBox, preserveAspectRatio = props.preserveAR,
                       ssr = props.ssr, onClick = props.onClick)
    let render props children = Mafs children props
