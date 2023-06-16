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
    }, []);
    
    useEffect(() => {
        handleFilter().catch(() => console.log());
    }, [filter])

    const fetchData = async () => {
        const {data} = await axios('api/employees');
        setData(data.result)
        setLoading(false)
    }

    const onChangeFilter = e => {
        const {name, value} = e.target;
        setFilter(prev => ({...prev, [name]: value}));
    };

    const handleFilter = async () => {
        const url = `api/employees?name=${filter.name}&surname=${filter.surname}&email=${filter.email}&orderBy=${filter.orderBy}`;
        await axios(url)
            .then((result) => {
                setData(result.data.result);
            }).catch((error) => {
                toast.error(error)
            })
    }

    const handleSort = (name, value) => {
        setSort(prev => ({...prev, [name]: !value}));
        const sortState = name + (value ? "Asc" : "Desc")
        setFilter(prev => ({...prev, orderBy: sortState}));
    }
    
    const getArrow = (condition) => {
        if (condition) {
            return (<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                 className="bi bi-arrow-up" viewBox="0 0 16 16">
                <path fillRule="evenodd"
                      d="M8 15a.5.5 0 0 0 .5-.5V2.707l3.146 3.147a.5.5 0 0 0 .708-.708l-4-4a.5.5 0 0 0-.708 0l-4 4a.5.5 0 1 0 .708.708L7.5 2.707V14.5a.5.5 0 0 0 .5.5z"/>
            </svg>)
        }
        return (<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
             className="bi bi-arrow-down" viewBox="0 0 16 16">
            <path fillRule="evenodd"
                  d="M8 1a.5.5 0 0 1 .5.5v11.793l3.146-3.147a.5.5 0 0 1 .708.708l-4 4a.5.5 0 0 1-.708 0l-4-4a.5.5 0 0 1 .708-.708L7.5 13.293V1.5A.5.5 0 0 1 8 1z"/>
        </svg>)
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
                                <th onClick={() => handleSort( "surname", sort.surname)}>Surname {getArrow(sort.surname)}</th>
                                <th onClick={() => handleSort("name", sort.name)}>Name</th>
                                <th>Patronymic</th>
                                <th onClick={() => handleSort("email", sort.email)}>Email</th>
                                <th>Actions</th>
                            </tr>
                            </thead>
                            <tbody>
                            {data.map((employee, index) => <tr key={index}>
                                <td>{employee.id}</td>
                                <td>{employee.surname}</td>
                                <td>{employee.name}</td>
                                <td>{employee.patronymic}</td>
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