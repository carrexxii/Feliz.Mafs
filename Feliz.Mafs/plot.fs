namespace Feliz.Mafs

open Fable.Core.JsInterop
open Feliz

module Plot =
    type Props =
        { fn      : float -> float
          color   : Color
          opacity : float
          weight  : float
          style   : LineStyle
          minDepth: float
          maxDepth: float }
        static member Default =
            { fn       = fun x -> x
              color    = Theme.foreground
              opacity  = 1.0
              weight   = 2.0
              style    = Solid
              minDepth = 8
              maxDepth = 15 }

    type InequalityPair =
        { lower: Inequality * (float -> float)
          upper: Inequality * (float -> float) }
        static member Default =
            { lower = GT, (fun x -> x)
              upper = LTEQ, (fun y -> 5) }

    let create fn = { Props.Default with fn = fn }
    let color    color   props = { props with color    = color   }
    let weight   weight  props = { props with weight   = weight  }
    let opacity  opacity props = { props with opacity  = opacity }
    let style    style   props = { props with style    = style   }
    let minDepth depth   props = { props with minDepth = depth   }
    let maxDepth depth   props = { props with maxDepth = depth   }

    [<ReactComponent>]
    let render axis props =
        match axis with
        | XAxis -> Verbatim.Plot.OfX (props.fn, color = props.color, weight = props.weight, opacity = props.opacity, style = props.style,
                                      minSamplingDepth = props.minDepth, maxSamplingDepth = props.maxDepth)
        | YAxis -> Verbatim.Plot.OfY (props.fn, color = props.color, weight = props.weight, opacity = props.opacity, style = props.style,
                                      minSamplingDepth = props.minDepth, maxSamplingDepth = props.maxDepth)

    let renderInequality axis leq ueq lprop uprop =
        match axis with
        | XAxis -> Verbatim.Plot.Inequality (y = createObj [ leq.ToString () ==> lprop.fn; ueq.ToString () ==> uprop.fn ],
                                             lowerColor = lprop.color, lowerWeight = lprop.weight,lowerOpacity = lprop.opacity,
                                             upperColor = uprop.color, upperWeight = uprop.weight, upperOpacity = uprop.opacity)
        | YAxis -> Verbatim.Plot.Inequality (x = createObj [ leq.ToString () ==> lprop.fn; ueq.ToString () ==> uprop.fn ],
                                             lowerColor = lprop.color, lowerWeight = lprop.weight,lowerOpacity = lprop.opacity,
                                             upperColor = uprop.color, upperWeight = uprop.weight, upperOpacity = uprop.opacity)

    module VectorField =
        type Props =
            { fn         : Vec2 -> Vec2
              opacity    : Vec2 -> float
              step       : float
              opacityStep: float
              color      : Color }
            static member Default =
                { fn          = fun p -> p
                  opacity     = fun p -> 1.0
                  step        = 1.0
                  opacityStep = 1.0
                  color       = Theme.foreground }

        let create fn = { Props.Default with fn = fn }
        let opacity     opacity     props = { props with opacity     = opacity     }: Props
        let step        step        props = { props with step        = step        }: Props
        let opacityStep opacityStep props = { props with opacityStep = opacityStep }: Props
        let color       color       props = { props with color       = color       }: Props

        [<ReactComponent>]
        let render props =
            let xy p = props.fn (Vec2 p) |> _.array
            let opacity p = props.opacity (Vec2 p)
            Verbatim.Plot.VectorField (xy, xyOpacity = opacity, step = props.step,
                                       opacityStep = props.opacityStep, color = props.color)
