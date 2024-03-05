namespace Feliz.Mafs.Docs

open System

open Fable.Core.JsInterop
open Feliz
open Feliz.Router
open Feliz.Mafs

[<AutoOpen>]
module Pages =
    let IndexPage () =
        let link href text =
            Html.li [
                prop.className "p-2 text-center text-lg"
                prop.children [
                    Html.a [
                        prop.href (string href)
                        prop.text (string text)
                    ]
                ]
            ]
        Html.div [
            Html.ul [
                prop.className "mt-12"
                prop.children [
                    link (Router.format "getstarted")  "Get Started"
                    link (Router.format "text")        "Text Examples"
                    link (Router.format "coordinates") "Coordinate Examples"
                    link (Router.format "camera")      "Camera Examples"
                    link (Router.format "plots")       "Plot Examples"
                ]
            ]
        ]

    [<ReactComponent>]
    let GetStartedPage () =
        Html.div [
            Heading "Get Started Examples"

            SubHeading "Drawing a coordinate plane"
            Mafs.create ()
            |> Mafs.render [
                Cartesian.create () |> Cartesian.render
            ]
            CodeBlock
                """ import { Mafs, Coordinates } from "mafs"

                    function HelloFx() {
                        return (
                            <Mafs>
                                <Coordinates.Cartesian />
                            </Mafs>
                        )
                    }
                """

            Mafs.create ()
            |> Mafs.render [
                Cartesian.create ()
                |> Cartesian.subDiv 4
                |> Cartesian.render
            ]
            CodeBlock
                """ import { Mafs, Coordinates } from "mafs"

                    function HelloFx() {
                        return (
                            <Mafs>
                                <Coordinates.Cartesian subdivisions={4} />
                            </Mafs>
                        )
                    }
                """

            SubHeading "Plotting a function"
            Mafs.create ()
            |> Mafs.render [
                Cartesian.create ()
                |> Cartesian.subDiv 4
                |> Cartesian.render

                Plot.create <| fun x -> sin x
                |> Plot.render XAxis
            ]
            CodeBlock
                """ import { Mafs, Coordinates, Plot } from "mafs"

                    function HelloFx() {
                        return (
                            <Mafs>
                                <Coordinates.Cartesian subdivisions={4} />
                                <Plot.OfX y={(x) => Math.sin(x)} />
                            </Mafs>
                        )
                    }
                """

            Mafs.create ()
            |> Mafs.viewBox {| padding = 0.5
                               x = vec -10 10
                               y = vec -2   2 |}
            |> Mafs.preserveAR false
            |> Mafs.render [
                Cartesian.create ()
                |> Cartesian.xAxis (Some { labels = Labels.Pi; lines = Math.PI })
                |> Cartesian.render

                Plot.create (fun x -> sin x)
                |> Plot.render XAxis
            ]
            CodeBlock
                """ import { Mafs, Coordinates, Plot, labelPi } from "mafs"

                    function HelloFx() {
                        return (
                            <Mafs
                                viewBox={{ x: [-10, 10], y: [-2, 2] }}
                                preserveAspectRatio={false}
                            >
                                <Coordinates.Cartesian
                                    subdivisions={4}
                                    xAxis={{ lines: Math.PI, labels: labelPi }}
                                />
                                <Plot.OfX y={(x) => Math.sin(x)} />
                            </Mafs>
                        )
                    }
                """

            SubHeading "Making things interactive"
            let phase = movablePoint (vec 0 0) Theme.foreground None
            Mafs.create ()
            |> Mafs.viewBox {| padding = 0.5
                               x = vec -10 10
                               y = vec -2   2 |}
            |> Mafs.preserveAR false
            |> Mafs.render [
                Cartesian.create ()
                |> Cartesian.xAxis (Some { labels = Labels.Pi; lines = Math.PI })
                |> Cartesian.subDiv 4
                |> Cartesian.render

                Plot.create (fun x -> sin (x - phase.x))
                |> Plot.render XAxis
                phase.element
            ]
            CodeBlock
                """ import { Mafs, Coordinates, Plot, labelPi, useMovablePoint } from "mafs"

                    function HelloFx() {
                        const phase = useMovablePoint([0, 0], {
                            constrain: "horizontal",
                        })

                        return (
                            <Mafs
                                viewBox={{ x: [-10, 10], y: [-2, 2] }}
                                preserveAspectRatio={false}
                            >
                            <Coordinates.Cartesian
                                subdivisions={4}
                                xAxis={{ lines: Math.PI, labels: labelPi }}
                            />
                            <Plot.OfX y={(x) => Math.sin(x - phase.x)} />
                            {phase.element}
                            </Mafs>
                        )
                    }
                """
        ]

    [<ReactComponent>]
    let TextPage () =
        Html.div [
            Heading "Text"
            Mafs.create ()
            |> Mafs.render [
                Text.create "I love maths!"
                |> Text.pos (vec 0 0)
                |> Text.render
            ]
            CodeBlock
                """ import { Mafs, Text } from "mafs"

                    function Example() {
                        return (
                            <Mafs>
                                <Text x={0} y={0}>I love math!</Text>
                            </Mafs>
                        )
                    }
                """
        ]

    [<ReactComponent>]
    let CameraPage () =
        Html.div [
            Heading "Camera Controls"
            Mafs.create ()
            |> Mafs.zoom 0.1 2.0
            |> Mafs.viewBox {| padding = 0
                               x = vec -0.25 0.25
                               y = vec -0.25 0.25 |}
            |> Mafs.render [
                Cartesian.create ()
                |> Cartesian.subDiv 4
                |> Cartesian.render

                Circle.create 1
                |> Circle.center (vec 0 0)
                |> Circle.render

                Text.create "Oh hi!"
                |> Text.pos (vec 1.1 0.1)
                |> Text.attach NorthEast 0.0
                |> Text.render
            ]
            CodeBlock
                """ import { Mafs, Coordinates, Circle, Text } from "mafs"

                    function ZoomExample() {
                        return (
                            <Mafs
                                zoom={{ min: 0.1, max: 2 }}
                                viewBox={{
                                    x: [-0.25, 0.25],
                                    y: [-0.25, 0.25],
                                    padding: 0,
                                }}
                            >
                                <Coordinates.Cartesian subdivisions={5} />
                                <Circle center={[0, 0]} radius={1} />
                                <Text x={1.1} y={0.1} attach="ne">
                                    Oh hi!
                                </Text>
                            </Mafs>
                        )
                    }
                """

            Mafs.create ()
            |> Mafs.viewBox {| padding = 0
                               x = vec -5 5
                               y = vec -5 5 |}
            |> Mafs.render [
                Cartesian.create () |> Cartesian.render

                Polygon.create [ vec -5 -5; vec  5 -5
                                 vec  5  5; vec -5  5 ]
                |> Polygon.render true
            ]
            CodeBlock
                """ import { Mafs, Coordinates, Polygon } from "mafs"

                    function ViewboxEample() {
                        return (
                            <Mafs viewBox={{ x: [-5, 5], y: [-5, 5] }}>
                                <Coordinates.Cartesian />
                                <Polygon points={[[-5, -5], [5, -5], [5, 5], [-5, 5]]} />
                            </Mafs>
                        )
                    }
                """
        ]

    [<ReactComponent>]
    let CoordinatesPage () =
        Html.div [
            Heading "Camera Controls"
            Mafs.create ()
            |> Mafs.render [
                Polar.create ()
                |> Polar.subDiv 5
                |> Polar.lines 2
                |> Polar.render
            ]
            CodeBlock
                """ import { Mafs, Coordinates } from "mafs"

                    function Example() {
                        return (
                            <Mafs>
                                <Coordinates.Polar subdivisions={5} lines={2} />
                            </Mafs>
                        )
                    }
                """
        ]

    [<ReactComponent>]
    let PlotsPage () =
        Html.div [
            Heading "Plots"
            Mafs.create ()
            |> Mafs.render [
                Cartesian.create () |> Cartesian.render

                Plot.create sin |> Plot.render XAxis
                Plot.create (fun x -> 2.0 / (1.0 + (exp -x)) - 1.0)
                |> Plot.color "magenta"
                |> Plot.render YAxis
            ]
            CodeBlock
                """ import { Mafs, Coordinates, Plot, Theme } from "mafs"

                    function FunctionsOfXAndY() {
                        const sigmoid1 = (x: number) => 2 / (1 + Math.exp(-x)) - 1

                        return (
                            <Mafs>
                                <Coordinates.Cartesian />
                                <Plot.OfX y={Math.sin} color={Theme.blue} />
                                <Plot.OfY x={sigmoid1} color={Theme.pink} />
                            </Mafs>
                        )
                    }
                """

            Mafs.create ()
            |> Mafs.render [
                Cartesian.create () |> Cartesian.render

                let point = movablePoint (vec 0 0) Theme.foreground None
                Plot.renderInequality YAxis LTEQ GT
                    (Plot.create (fun y -> cos (y + point.y) - point.x))
                    (Plot.create (fun y -> sin (y - point.y) + point.x))

                Plot.renderInequality XAxis LTEQ GT
                    (Plot.create (fun x -> cos (x + point.x) - point.y)
                    |> Plot.color Theme.pink)
                    (Plot.create (fun x -> sin (x - point.x) + point.y)
                    |> Plot.color Theme.pink)

                point.element
            ]
            CodeBlock
                """ import { Mafs, Coordinates, Plot, Theme, useMovablePoint } from "mafs"

                    function InequalitiesExample() {
                        const a = useMovablePoint([0, -1])

                        return (
                            <Mafs>
                                <Coordinates.Cartesian />

                                <Plot.Inequality
                                    x={{
                                        "<=": (y) => Math.cos(y + a.y) - a.x,
                                        ">": (y) => Math.sin(y - a.y) + a.x,
                                    }}
                                    color={Theme.blue}
                                />

                                <Plot.Inequality
                                    y={{
                                    "<=": (x) => Math.cos(x + a.x) - a.y,
                                    ">": (x) => Math.sin(x - a.x) + a.y,
                                    }}
                                    color={Theme.pink}
                                />

                                {a.element}
                            </Mafs>
                        )
                    }
                """
        ]
