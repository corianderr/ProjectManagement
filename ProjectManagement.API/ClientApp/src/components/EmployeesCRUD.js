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
    const [filter, setFilter] = useState({
        name: "",
        surname: "",
        email: "",
        orderBy: ""
    });
    const [sort, setSort] = useState({
        name: true,
        surname: true,
        email: true,
    });


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

    const onChangeFilter = e => {
        const {name, value} = e.target;
        setFilter(prev => ({...prev, [name]: value}));
    };

    const handleFilter = async (e) => {
        e.preventDefault();
        const url = `api/employees?name=${filter.name}&surname=${filter.surname}&email=${filter.email}&orderBy=${filter.orderBy}`;
        await axios(url)
            .then((result) => {
                setData(result.data.result);
            }).catch((error) => {
                toast.error(error)
            })
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
                                <Form.Control name="name" type="text" onChange={onChangeFilter}/>
                            </Col>
                            <Col>
                                <Form.Label>Surname</Form.Label>
                                <Form.Control name="surname" type="text" onChange={onChangeFilter}/>
                            </Col>
                            <Col>
                                <Form.Label>Email</Form.Label>
                                <Form.Control name="email" type="text" onChange={onChangeFilter}/>
                                {isVisible && <Confetti/>}
                            </Col>
                            <Col>
                                <div className="h-100 d-flex">
                                    <Button className="mt-auto px-4" variant="secondary"
                                            onClick={(e) => handleFilter(e)}>Filter</Button>
                                </div>
                            </Col>
                        </Row>
                        <table className='table table-striped text-center' aria-labelledby="tabelLabel">
                            <thead>
                            <tr>
                                <th>Id</th>
                                <th>Full Name</th>
                                <th>Email</th>
                                <th>Actions</th>
                            </tr>
                            </thead>
                            <tbody>
                            {data.map((employee, index) => <tr key={index}>
                                <td>{employee.id}</td>
                                <td>{employee.name} {employee.surname} {employee.patronymic}</td>
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