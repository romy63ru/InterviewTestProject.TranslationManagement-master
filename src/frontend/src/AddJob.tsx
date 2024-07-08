import React from "react";
import axios from "axios";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

class AddJob extends React.Component {
  state = {
    customerName: "",
    status: "",
    originalContent: "",
    translatedContent: "",
    price: 0,
  };

  handleChange = (event) => {
    const { name, value } = event.target;
    this.setState({
      [name]: name === "price" ? parseFloat(value) || 0 : value,
    });
  };

  handleSubmit = async (event) => {
    event.preventDefault();
    const { customerName, status, originalContent, translatedContent, price } = this.state;

    try {
      const res = await axios.post(`${process.env.REACT_APP_API_URL}/api/jobs/CreateJob`, {
        customerName,
        status,
        originalContent,
        translatedContent,
        price,
      });
      // Handle success (e.g., notify user, clear form)
    } catch (error) {
        console.error(error);
        toast.error("An error occurred. Please try again.");
      }
    }
  

  render(): JSX.Element {
    const { customerName, status, originalContent, translatedContent, price } = this.state;
    return (
      <div>
        <form onSubmit={this.handleSubmit}>
          <label>
            Customer Name:
            <input type="text" name="customerName" value={customerName} onChange={this.handleChange} />
          </label>
          <label>
            Status:
            <input type="text" name="status" value={status} onChange={this.handleChange} />
          </label>
          <label>
            Original Content:
            <textarea name="originalContent" value={originalContent} onChange={this.handleChange} />
          </label>
          <label>
            Translated Content:
            <textarea name="translatedContent" value={translatedContent} onChange={this.handleChange} />
          </label>
          <label>
            Price:
            <input type="number" name="price" value={price} onChange={this.handleChange} />
          </label>
          <button type="submit">Add Job</button>
        </form>
      </div>
    );
  }
}

export default AddJob;
