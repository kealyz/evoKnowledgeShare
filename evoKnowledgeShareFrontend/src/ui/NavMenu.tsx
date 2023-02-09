import { motion } from 'framer-motion';
import { Container, Dropdown, Nav, Navbar, NavDropdown } from 'react-bootstrap';
import { Link, NavLink } from 'react-router-dom';

export default function NavMenu() {
    return (
        <Navbar bg="primary" variant="dark">
            <Container>
                <Navbar.Brand href="/">evoKnowledgeShare</Navbar.Brand>
                <Nav className="me-auto">
                    <Link to="/" className="nav-link">Home</Link>
                    <Link to="/Editor" className="nav-link">Editor</Link>
                    <NavDropdown title="Operations" id="basic-nav-dropdown">
                        <NavDropdown.Item><Link style={{ color: "black" }} className="nav-link" to="/Histories">History Actions</Link></NavDropdown.Item>
                        <NavDropdown.Divider />
                        <NavDropdown.Item><Link style={{ color: "black" }} className="nav-link" to="/Topics">Topic Actions</Link></NavDropdown.Item>
                        <NavDropdown.Divider />
                        <NavDropdown.Item><Link style={{ color: "black" }} className="nav-link" to="/Users">User Actions</Link></NavDropdown.Item>
                        <NavDropdown.Divider />
                        <NavDropdown.Item><Link style={{ color: "black" }} className="nav-link" to="/Notes">Notes</Link></NavDropdown.Item>
                    </NavDropdown>
                </Nav>
            </Container>
        </Navbar>
    );
}