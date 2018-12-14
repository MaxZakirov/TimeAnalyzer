import * as React from "react";
import { Bar, Pie, Doughnut, Radar, Polar } from 'react-chartjs-2';
import TimeReportApiService from './TimeReportApiService'
import ChangeValueForm from "./ChangeValueForm";



export default class Chart extends React.Component<any, any> {

    render() {
        return (
            <div className="mainPage">
                    
                        <div className="chart">
                            <Pie
                                data={{
                                    
                                    labels: ["parasha","pizda","huy"],
                                    datasets: [{
                                        label: 'Power',
                                        data: [123,234,345],
                                        backgroundColor: ["#fff","#ddd","#bbb"],
                                        borderWidth: 2,
                                        borderColor: '#fff',
                                        hoverBorderWidth: 2,
                                        hoverBorderColor: '#eee'
                                    }]
                                }}
                                options={{
                                    responsive:true,
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
                        <div className="chart">
                            <Bar
                                data={{
                                    labels: ["parasha","pizda","huy"],
                                    datasets: [{
                                        label: '',
                                        data: [123,234,345],
                                        backgroundColor: ["#fff","#ddd","#bbb"],
                                        borderWidth: 2,
                                        borderColor: '#fff',
                                        hoverBorderWidth: 2,
                                        hoverBorderColor: '#eee'
                                    }]
                                }}
                                options={{
                                    
                                }}
                            />
                        </div>


                    <div>
                        {/* <ChangeValueForm/> */}
                    </div>
                    

            </div>
        )
    }
}