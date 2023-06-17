import React, {Component} from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div className="text-center container">
                <h1>Hi !</h1>
                <p>(Links are on the top)</p>
                <br/>
                <h2 className="text-center">Welcome to Karina's Project Managment App !!</h2>
                <p>
                    ProjectManagement is a game-changing project management solution that redefines how you handle
                    projects. With its cutting-edge three-tier architecture, it empowers you to take control and
                    drive success. Seamlessly access and manage project data with lightning speed through the Data
                    Access Level (DAL). Elevate your project's logic and fine-tune every aspect with the Business
                    Access Level (BAL) and comprehensive unit tests. Achieve seamless integration and collaboration
                    using the Application Programming Interface (API). Securely store your data with SQLite. Engage
                    your team with a visually captivating frontend powered by ReactJS. Experience the future of project
                    management with ProjectManagement and unlock your project's true potential.
                </p>
                <br/>
                <h3>Here are the technologies that were used:</h3>
                <div className="row m-auto">
                    <div className="ms-auto col-3">
                        <p>Backend</p>
                        <ul className="technologies">
                            <li>.NET 6</li>
                            <li>ASP.NET Core 6</li>
                            <li>Entity Framework Core</li>
                            <li>AutoMapper</li>
                            <li>XUnit (Unit tests)</li>
                            <li>FluentAssertion (Testing project)</li>
                            <li>NBuilder (Testing project)</li>
                        </ul>
                    </div>
                    <div className="me-auto col-3">
                        <p>Frontend</p>
                        <ul className="front-technologies">
                            <li>ReactJS</li>
                            <li>React Toastify</li>
                            <li>React Bootstrap</li>
                            <li>Axios</li>
                        </ul>
                    </div>
                </div>
            </div>
        );
    }
}
