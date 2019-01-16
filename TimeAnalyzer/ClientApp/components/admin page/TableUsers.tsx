import * as React from 'react';
import UsersService from '../services/UsersService';

export default class TableUsers extends React.Component<any, any>{

    userService: UsersService;
    constructor(props: any) {
        super(props)
        this.state = {
            users: []
        }
        this.userService = new UsersService();
    }

    componentDidMount() {

        this.userService.getAllUsers()
            .then((res: any) => {
                this.setState({
                    users: res.data
                });
            });
    }

    deleteUser(user: any) {
        this.userService.deleteUser(user)
            .then((res: any) => {
                this.setState({
                    users: this.state.users.filter((u: any) => u.id !== user.id)
                });
            });
    }

    render() {
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
                                this.state.users.map((item: any) => {
                                    return (
                                        <tr key={item._id}>
                                            <td className="col-md-4">{item.name}</td>
                                            <td className="col-md-4">{item.email}</td>
                                            <td className="col-md-4">
                                                <button className="deleteButton" onClick={() => this.deleteUser(item)}>
                                                    delete
                                                </button>
                                            </td>
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