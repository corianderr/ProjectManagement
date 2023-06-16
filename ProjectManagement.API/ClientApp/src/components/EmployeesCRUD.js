import React, {useEffect, useState} from 'react';
import {toast, ToastContainer} from "react-toastify";
import axios from "axios";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Form from "react-bootstrap/Form";
import Confetti from "./Confetti";
import Button from "react-bootstrap/Button";
import {Link} from "react-router-dom";
import Modal from "react-bootstrap/Modal";

const EmployeesCrud = () => {
    const [loading, setLoading] = useState(true)
    const [data, setData] = useState([]);
    const [isVisible, setIsVisible] = useState(false);

    useEffect(() => {
        try {
            fetchData().catch((e) => console.log())
        } catch (error) {
            toast.error(error)
            setLoading(false)
        }
    }, [])

    const fetchData = async () => {
        const {data} = await axios('api/employees');
        setData(data.result)
        setLoading(false)
    }

    return (
        <div>
            <ToastContainer/>
            {
                loading ? <p><em>Loading...</em></p> :
                    <div>
                        <Row>
                            <Col>
                                <Form.Label>Name</Form.Label>
                                <Form.Control name="name" type="text"/>
                            </Col>
                            <Col>
                                <Form.Label>Surname</Form.Label>
                                <Form.Control name="surname" type="text"/>
                            </Col>
                            <Col>
                                <Form.Label>Email</Form.Label>
                                <Form.Control name="email" type="text"/>
                                {isVisible && <Confetti/>}
                            </Col>
                            <Col>
                                <div className="h-100 d-flex">
                                    <Button className="mt-auto px-4" variant="secondary">Filter</Button>
                                </div>
                            </Col>
                        </Row>
                        <table className='table table-striped text-center' aria-labelledby="tabelLabel">
                            <thead>
                            <tr>
                                <th>Id</th>
                                <th>Name</th>
                                <th>Surname</th>
                                <th>Email</th>
                                <th>Actions</th>
                            </tr>
                            </thead>
                            <tbody>
                            {data.map((employee, index) => <tr key={index}>
                                <td>{employee.id}</td>
                                <td><Link tag={Link} className="text-dark"
                                          to={`/getById/${employee.id}`}>{employee.name}</Link></td>
                                <td>{employee.surname}</td>
                                <td>{employee.email}</td>
                                <td>
                                    <button className="btn btn-primary px-4 me-2">Edit
                                    </button>
                                    <button className="btn btn-danger px-4 ms-2">Delete
                                    </button>
                                </td>
                            </tr>)}
                            </tbody>
                        </table>
                    </div>
            }
        </div>);
};

export default EmployeesCrud;