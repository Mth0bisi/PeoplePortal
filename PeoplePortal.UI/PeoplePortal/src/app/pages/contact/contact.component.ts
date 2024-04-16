import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent implements OnInit {
  contactForm!: UntypedFormGroup;
  contactLinks: { label: string, url: string }[] = [
    { label: 'Email: mailto:info@peopleportal.co.za', url: 'mailto:info@peopleportal.co.za' },
    { label: 'Phone: 0860PEOPLE', url: 'tel:+27860234234' },
    { label: 'Website: www.peopleportal.co.za', url: 'https://www.youtube.com/watch?v=AfIOBLr1NDU&ab_channel=fpSmokerO' }
    // Add more links as needed
  ];

  constructor(private formBuilder: UntypedFormBuilder) {
    this.contactForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      message: ['', [Validators.required, Validators.minLength(10)]]
    })
  }


  enviar(event: Event) {
    event.preventDefault();
    console.log(this.contactForm.value);
  }

  ngOnInit(): void {

  }

  hasErrors(field: string, typeError: string) {
    return this.contactForm.get(field)?.hasError(typeError) && this.contactForm.get(field)?.touched;
  }

}
