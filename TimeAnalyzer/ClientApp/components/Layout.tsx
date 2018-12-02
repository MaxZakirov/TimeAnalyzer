import * as React from 'react';
import AuthService from './AuthService';





export class Layout extends React.Component<{}, {}> {
     public render() {
        const Auth = new AuthService();

            return <div>
            <div>
                <div>
                    { this.props.children }
                </div>
            </div>
        </div>;

        }
    }

