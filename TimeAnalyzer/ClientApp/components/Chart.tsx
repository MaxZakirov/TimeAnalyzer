import * as React from "react";
import { Pie, Doughnut, Radar, Polar } from 'react-chartjs-2';
import TimeReportApiService from './TimeReportApiService'
import ChangeValueForm from "./ChangeValueForm";



export default class Chart extends React.Component<any, any> {

    render() {
        return (
            <div className="mainPage">

                 <div className="center">
                    <div className="chartContainer">
                        <div className="chart">
                            <Doughnut
                                data={{
                                    labels: this.state.chartData.map((dataObject: any) => dataObject.initialChartData.Name),
                                    datasets: [{
                                        label: 'Power',
                                        data: this.state.chartData.map((dataObject: any) => dataObject.initialChartData.Duration),
                                        backgroundColor: this.state.chartData.map((dataObject: any) => dataObject.Activity.ColorValue),
                                        borderWidth: 2,
                                        borderColor: '#fff',
                                        hoverBorderWidth: 2,
                                        hoverBorderColor: '#eee'
                                    }]
                                }}
                                options={{
                                    legend: {
                                        display: true,
                                        position: 'left',
                                        labels: {
                                            fontSize: 15,
                                            fontColor: '#eee'
                                        }
                                    }
                                }}
                            />
                        </div>
                    </div>

                    <div>
                        <ChangeValueForm/>
                    </div>
                    
                </div> 
            </div>
        )
    }
}