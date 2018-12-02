import * as React from "react"
import { Pie, Doughnut, Radar, Polar } from 'react-chartjs-2'


export default class Chart extends React.Component<any, any> {

    drus = 100;
    liokha = 200;
    denis = 300;
    misha = 100;
    tigran = 200;
    tyulya = 300;
    mops = 400;


    constructor(props:any) {
        super(props);
        this.state = {
            fields: {},
            name: '',
            selectedOption: 'option1'
        }

        this.handleChange = this.handleChange.bind(this);
        this.submit = this.submit.bind(this);
    }

    handleChange(e:any) {
        let fields: any = this.state.fields;
        fields[e.target.name] = e.target.value;
        this.setState({
            fields,
            selectedOption: e.target.value
        });
    }



    submit(e:any) {
        e.preventDefault();
        if (this.validate()) {
            let fields: any = {};
            this.setState({ fields: fields });
            fields["field"] = "";
        }

    }

    validate() {
        let fields:any = this.state.fields;
        let formIsValid = true;

        if (this.state.selectedOption === 'option1') {
            formIsValid = true;
            this.mops = +fields["field"];
            console.log(this.mops)
        } else
            if (this.state.selectedOption === 'option2') {
                formIsValid = true;
                this.liokha = +fields["field"];
                console.log(this.liokha)
            } else
                if (this.state.selectedOption === 'option3') {
                    formIsValid = true;
                    this.denis = +fields["field"];
                    console.log(this.denis)
                } else
                    if (this.state.selectedOption === 'option4') {
                        formIsValid = true;
                        this.drus = +fields["field"];
                        console.log(this.drus)
                    } else
                        if (this.state.selectedOption === 'option5') {
                            formIsValid = true;
                            this.misha = +fields["field"];
                            console.log(this.misha)
                        } else
                            if (this.state.selectedOption === 'option6') {
                                formIsValid = true;
                                this.tigran = +fields["field"];
                                console.log(this.tigran)
                            } else
                                if (this.state.selectedOption === 'option7') {
                                    formIsValid = true;
                                    this.tyulya = +fields["field"];
                                    console.log(this.tyulya)
                                }

        return formIsValid;

    }

    render() {
        return (
            <div className="container">

                <div className="chart">
                    <Doughnut
                        data={{
                            labels: ['Mops', 'Drus', 'Liokha', 'Denis', 'Misha', 'Tigran', 'Tyulya'],
                            datasets: [{
                                label: 'Power',
                                data: [
                                    this.mops,
                                    this.drus,
                                    this.liokha,
                                    this.denis,
                                    this.misha,
                                    this.tigran,
                                    this.tyulya
                                ],
                                backgroundColor: [
                                    '#F44336',
                                    '#9C27B0',
                                    '#FFFF00',
                                    '#2962FF',
                                    '#FF6E40',
                                    '#D50000',
                                    'rgba(0,255,255,0.6)'
                                ],
                                borderWidth: 0,
                                borderColor: '#fff',
                                hoverBorderWidth: 2,
                                hoverBorderColor: '#999'
                            }]
                        }}
                        options={{
                            title: {
                                display: true,
                                text: 'The POWER of Gays',
                                fontSize: 25
                            },
                            legend: {
                                display: true,
                                position: 'left',
                                labels: {
                                    fontSize: 15,
                                    fontColor: '#000'
                                }
                            }
                        }}
                    />
                </div>

                <div className="form">
                    <form onSubmit={this.submit}>
                        <p>
                            <input
                                className="check"
                                value="option1"
                                name="check" type="radio"
                                checked={this.state.selectedOption === 'option1'}
                                onChange={this.handleChange}>
                            </input>
                            <label>Mops</label>
                        </p>

                        <p>
                            <input
                                className="check"
                                value="option2"
                                name="check"
                                type="radio"
                                checked={this.state.selectedOption === 'option2'}
                                onChange={this.handleChange}>
                            </input>
                            <label>Liokha</label>
                        </p>

                        <p>
                            <input
                                className="check"
                                value="option3"
                                name="check"
                                type="radio"
                                checked={this.state.selectedOption === 'option3'}
                                onChange={this.handleChange}>
                            </input>
                            <label>Denis</label>
                        </p>

                        <p>
                            <input
                                className="check"
                                value="option4"
                                name="check"
                                type="radio"
                                checked={this.state.selectedOption === 'option4'}
                                onChange={this.handleChange}>
                            </input>
                            <label>Drus</label>
                        </p>

                        <p>
                            <input
                                className="check"
                                value="option5"
                                name="check"
                                type="radio"
                                checked={this.state.selectedOption === 'option5'}
                                onChange={this.handleChange}>
                            </input>
                            <label>Misha</label>
                        </p>

                        <p>
                            <input
                                className="check"
                                value="option6"
                                name="check"
                                type="radio"
                                checked={this.state.selectedOption === 'option6'}
                                onChange={this.handleChange}>
                            </input>
                            <label>Tigran</label>
                        </p>

                        <p>
                            <input
                                className="check"
                                value="option7"
                                name="check"
                                type="radio"
                                checked={this.state.selectedOption === 'option7'}
                                onChange={this.handleChange}>
                            </input>
                            <label>Tyulya</label>
                        </p>

                        <input type="text" name="field" value={this.state.fields.field || ""} onChange={this.handleChange} placeholder="type your value"></input>
                        <button type="submit">Change value</button>
                    </form>
                </div>

            </div>
        )
    }
}