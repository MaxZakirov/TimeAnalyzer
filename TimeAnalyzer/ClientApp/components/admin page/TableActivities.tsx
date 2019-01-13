import * as React from 'react';
import ActivitiesService from '../services/ActivitiesService';

export default class TableActivities extends React.Component<any, any>{

    service: ActivitiesService;
    constructor(props: any) {
        super(props)
        this.state = {
            data: []
        }
        this.service = new ActivitiesService();
    }

    componentDidMount() {
        this.service.getAllActivities()
            .then((res: any) => {
                this.setState({
                    data: res.data
                });
            });

    }

    render() {
        const { data } = this.state
        return (
            <div>
                <label className="tableLabels">Activities</label>
                <div className="scrolltable">
                    <table className='table table-bordered table-striped'>

                        <thead>
                            <tr>
                                <th className="col-md-6">Name</th>
                                <th className="col-md-6">Activity type</th>
                            </tr>

                        </thead>
                        <tbody>
                            {

                                data.map((item: any) => {
                                    return (
                                        <tr key={item._id}>
                                            <td className="col-md-6">{item.name}</td>
                                            <td className="col-md-6">{item.type.name}</td>
                                        </tr>
                                    )
                                })
                            }
                        </tbody>
                    </table>
                </div>
            </div>

        )


    }



}