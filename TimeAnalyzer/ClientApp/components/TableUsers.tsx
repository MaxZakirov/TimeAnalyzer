import * as React from 'react';
import UsersService from './services/UsersService';
import UserDeleteButton from './UserDeleteButton';

export default class TableUsers extends React.Component<any, any>{

    service:UsersService;
    constructor(props: any) {
        super(props)
        this.state = {
            data: []
        }
        this.service = new UsersService();
    }    

     componentDidMount() {

         this.service.getAllUsers()
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
                <label className="tableLabels">Users</label>
                <div className="scrolltable">
                    <table className='table table-bordered'>

                        <thead>
                            <tr>
                                <th className="col-md-4">name</th>
                                <th className="col-md-4">email</th>
                                <th className="col-md-4">delete</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                data.map((item: any) => {
                                    return (
                                            <tr key={item._id}>
                                                <td className="col-md-4">{item.name}</td>
                                                <td className="col-md-4">{item.email}</td>
                                                <td className="col-md-4">{<UserDeleteButton/>}</td>
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