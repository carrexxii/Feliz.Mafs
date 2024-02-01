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
            Mafs MafsProps.Default [
                CartesianDefault
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

            Mafs MafsProps.Default [
                Cartesian { CartesianProps.Default with subDiv = 4 }
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
            Mafs MafsProps.Default [
                Cartesian { CartesianProps.Default with subDiv = 4 }

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

            Mafs { MafsProps.Default with
                    viewBox = { ViewBox.Default with
                                  x = [| -10; 10 |]
                                  y = [| -2 ;  2 |] }
                    preserveAspectRatio = !^false }
                [
                    Cartesian { xAxis  = !^{ lines  = Math.PI
                                             labels = Labels.Pi }
                                yAxis  = !^Axis.Default
                                subDiv = 4 }

                    Plot.create <| fun x -> sin x
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
            Mafs { MafsProps.Default with
                    viewBox = { ViewBox.Default with
                                  x = [| -10; 10 |]
                                  y = [| -2 ; 2  |] }
                    preserveAspectRatio = !^false }
                [
                    Cartesian { xAxis  = !^{ lines  = Math.PI
                                             labels = Labels.Pi }
                                yAxis  = !^Axis.Default
                                subDiv = 4 }

                    Plot.create <| fun x -> sin (x - phase.x)
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
            Mafs MafsProps.Default [
                Text.create "I love maths!" [| 0.0; 0.0 |]
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
            Mafs { MafsProps.Default with
                     zoom = !^{| min = 0.1; max = 2.0 |}
                     viewBox = { x = [| -0.25; 0.25 |]
                                 y = [| -0.25; 0.25 |]
                                 padding = 0 } } [
                Cartesian { CartesianProps.Default with subDiv = 4 }
                Circle [| 0; 0 |] 1
                Text.create "Oh hi!" [| 1.1; 0.1 |]
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

            Mafs { MafsProps.Default with
                    viewBox = { ViewBox.Default with
                                  x = [| -5; 5 |]
                                  y = [| -5; 5 |] } }
                [
                    Cartesian CartesianProps.Default
                    Polygon [|[| -5; -5 |]; [|  5; -5 |]
                              [|  5;  5 |]; [| -5;  5 |]|]
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
            Mafs MafsProps.Default [
                Polar { PolarProps.Default with
                          subDiv = 5
                          lines  = 2 }
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
            Mafs MafsProps.Default [
                Cartesian CartesianProps.Default

                Plot.create sin |> Plot.render XAxis
                Plot.create <| fun x -> 2.0 / (1.0 + (exp -x)) - 1.0
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

            Mafs MafsProps.Default [
                Cartesian CartesianProps.Default

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
